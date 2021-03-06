﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using Mono.Cecil;

namespace Mono.Linker.Steps
{
	public class RootAssemblyInput : BaseStep
	{
		readonly string fileName;
		readonly AssemblyRootMode rootMode;

		public RootAssemblyInput (string fileName, AssemblyRootMode rootMode)
		{
			this.fileName = fileName;
			this.rootMode = rootMode;
		}

		protected override void Process ()
		{
			AssemblyDefinition assembly = LoadAssemblyFile ();

			var di = new DependencyInfo (DependencyKind.RootAssembly, assembly);

			AssemblyAction action = Context.Annotations.GetAction (assembly);
			switch (action) {
			case AssemblyAction.Copy:
				Annotations.Mark (assembly.MainModule, di);
				// Mark Step will take care of marking whole assembly
				return;
			case AssemblyAction.CopyUsed:
			case AssemblyAction.Link:
				break;
			default:
				Context.LogError ($"Root assembly '{assembly.Name}' cannot use action '{action}'", 1035);
				return;
			}

			switch (rootMode) {
			case AssemblyRootMode.Default:
				if (assembly.MainModule.Kind == ModuleKind.Dll)
					goto case AssemblyRootMode.AllMembers;
				else
					goto case AssemblyRootMode.EntryPoint;
			case AssemblyRootMode.EntryPoint:
				var ep = assembly.MainModule.EntryPoint;
				if (ep == null) {
					Context.LogError ($"Root assembly '{assembly.Name}' does not have entry point", 1034);
					return;
				}

				Annotations.Mark (ep.DeclaringType, di);
				Annotations.AddPreservedMethod (ep.DeclaringType, ep);
				break;
			case AssemblyRootMode.VisibleMembers:
				var preserve = HasInternalsVisibleTo (assembly) ? TypePreserveMembers.AllVisibleOrInternal : TypePreserveMembers.AllVisible;

				var module = assembly.MainModule;
				if (module.HasExportedTypes)
					foreach (var type in module.ExportedTypes)
						MarkAndPreserveVisible (assembly, type, preserve);

				foreach (var type in module.Types)
					MarkAndPreserveVisible (type, preserve);
				break;
			case AssemblyRootMode.AllMembers:
				Context.Annotations.SetAction (assembly, AssemblyAction.Copy);
				return;
			}
		}

		AssemblyDefinition LoadAssemblyFile ()
		{
			AssemblyDefinition assembly = Context.Resolver.GetAssembly (fileName, Context.ReaderParameters);
			AssemblyDefinition loaded = Context.GetLoadedAssembly (assembly.Name.Name);

			// The same assembly could be already loaded if there are multiple inputs pointing to same file
			if (loaded != null)
				return loaded;

			Context.Resolver.CacheAssembly (assembly);
			Context.RegisterAssembly (assembly);
			return assembly;
		}

		void MarkAndPreserveVisible (TypeDefinition type, TypePreserveMembers preserve)
		{
			switch (preserve) {
			case TypePreserveMembers.AllVisible when !IsTypeVisible (type):
				return;
			case TypePreserveMembers.AllVisibleOrInternal when !IsTypeVisibleOrInternal (type):
				return;
			}

			Annotations.Mark (type, new DependencyInfo (DependencyKind.RootAssembly, type.Module.Assembly));
			Annotations.SetMembersPreserve (type, preserve);

			if (!type.HasNestedTypes)
				return;

			foreach (TypeDefinition nested in type.NestedTypes)
				MarkAndPreserveVisible (nested, preserve);
		}

		void MarkAndPreserveVisible (AssemblyDefinition assembly, ExportedType type, TypePreserveMembers preserve)
		{
			var di = new DependencyInfo (DependencyKind.RootAssembly, assembly);
			Context.Annotations.Mark (type, di);
			Context.Annotations.Mark (assembly.MainModule, di);
			Annotations.SetMembersPreserve (type, preserve);
		}

		static bool IsTypeVisible (TypeDefinition type)
		{
			return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamilyOrAssembly;
		}

		static bool IsTypeVisibleOrInternal (TypeDefinition type)
		{
			return !type.IsNestedPrivate;
		}

		static bool HasInternalsVisibleTo (AssemblyDefinition assembly)
		{
			foreach (CustomAttribute attribute in assembly.CustomAttributes) {
				if (attribute.Constructor.DeclaringType.IsTypeOf ("System.Runtime.CompilerServices", "InternalsVisibleToAttribute"))
					return true;
			}

			return false;
		}
	}
}
