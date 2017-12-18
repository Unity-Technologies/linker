using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Linker.Steps;

namespace Mono.Linker
{
	public class Dependencies
	{
		protected readonly LinkContext _context;
		Stack<object> dependency_stack;
		System.Xml.XmlWriter writer;
		GZipStream zipStream;

		public Dependencies(LinkContext context)
		{
			_context = context;
		}

		public void PrepareDependenciesDump()
		{
			PrepareDependenciesDump("linker-dependencies.xml.gz");
		}

		public void PrepareDependenciesDump(string filename)
		{
			dependency_stack = new Stack<object>();
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			var depsFile = File.OpenWrite(filename);
			zipStream = new GZipStream(depsFile, CompressionMode.Compress);

			writer = System.Xml.XmlWriter.Create(zipStream, settings);
			writer.WriteStartDocument();
			writer.WriteStartElement("dependencies");
			writer.WriteStartAttribute("version");
			writer.WriteString("1.0");
			writer.WriteEndAttribute();
		}

		public IDisposable BeginScope(TypeDefinition definition)
		{
			Push(definition);
			return new DependencyScope(this);
		}

		public IDisposable BeginScope(AssemblyDefinition definition)
		{
			Push(definition);
			return new DependencyScope(this);
		}

		public IDisposable BeginScope(IMemberDefinition definition)
		{
			Push(definition);
			return new DependencyScope(this);
		}

		public IDisposable BeginScope(IStep step)
		{
			Push(step);
			return new DependencyScope(this);
		}

		public void AddDirectDependency(object b, object e)
		{
			if (writer == null)
				return;

			//if (!ShouldRecordDependencyInformation(b) & !ShouldRecordDependencyInformation(e))
			//	return;

			WriteEdge(b, e);
		}

		public void AddDependency(object o)
		{
			if (writer == null)
				return;

			KeyValuePair<object, object> pair = new KeyValuePair<object, object>(dependency_stack.Count > 0 ? dependency_stack.Peek() : null, o);

			if (!ShouldRecordDependencyInformation(pair.Key) & !ShouldRecordDependencyInformation(pair.Value))
				return;

			if (pair.Key != pair.Value)
			{
				WriteEdge(pair.Key, pair.Value);
			}
		}

		public void Push(TypeDefinition definition)
		{
			if (writer == null)
				return;

			PushPrivate(definition, WillAssemblyBeModified(definition.Module.Assembly));
		}

		public void Push(MethodDefinition definition)
		{
			Push(definition as IMemberDefinition);
		}

		public void Push(PropertyDefinition definition)
		{
			Push(definition as IMemberDefinition);
		}

		public void Push(AssemblyDefinition definition)
		{
			if (writer == null)
				return;

			PushPrivate(definition, WillAssemblyBeModified(definition));
		}

		public void Push(ModuleDefinition definition)
		{
			if (writer == null)
				return;

			PushPrivate(definition, WillAssemblyBeModified(definition.Assembly));
		}

		public void Push(IMemberDefinition definition)
		{
			if (writer == null)
				return;

			PushPrivate(definition, WillAssemblyBeModified(definition.DeclaringType.Module.Assembly));
		}

		public void Push(TypeReference reference)
		{
			if (writer == null)
				return;

			if (reference is TypeDefinition definition)
			{
				Push(definition);
				return;
			}

			PushPrivate(reference, WillAssemblyBeModified(reference.Resolve().Module.Assembly));
		}

		public void Push(MemberReference reference)
		{
			if (writer == null)
				return;

			if (reference is IMemberDefinition definition)
			{
				Push(definition);
				return;
			}

			PushPrivate(reference, WillAssemblyBeModified(reference.Resolve().DeclaringType.Module.Assembly));
		}



		public void Push(ICustomAttributeProvider provider)
		{
			if (writer == null)
				return;

			// TODO by Mike : Not sure what to do about this one
			//if (!WillAssemblyBeModified(definition.DeclaringType.Module.Assembly))
			//	return;

			PushPrivate(provider, true);
		}

		public void Push(ExportedType exportedType)
		{
			if (writer == null)
				return;

			// TODO by Mike : Not sure what to do about this one
			//if (!WillAssemblyBeModified(exportedType.DeclaringType.Module.Assembly))
			//	return;

			PushPrivate(exportedType, true);
		}

		public void Push(IStep step)
		{
			if (writer == null)
				return;

			PushPrivate(step, true);
		}

		public void Push(object o)
		{
			if (writer == null)
				return;

			if (TryPushAsKnownType(o))
			{
				return;
			}

			PushPrivate(o, true);
		}

		public void Pop()
		{
			if (writer == null)
				return;

			dependency_stack.Pop();
		}

		public void SaveDependencies()
		{
			if (writer == null)
				return;

			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Dispose();
			zipStream.Dispose();
			writer = null;
			zipStream = null;
			dependency_stack = null;
		}

		void PushPrivate(object o, bool recordDep)
		{
			if (dependency_stack.Count > 0 && recordDep)
				AddDependency(o);
			dependency_stack.Push(o);
		}

		void WriteEdge(object b, object e)
		{
			writer.WriteStartElement("edge");
			writer.WriteAttributeString("b", TokenString(b));
			writer.WriteAttributeString("e", TokenString(e));
			writer.WriteEndElement();
		}

		string TokenString(object o)
		{
			if (o == null)
				return "N:null";

			if (o is IMetadataTokenProvider)
				return (o as IMetadataTokenProvider).MetadataToken.TokenType + ":" + o;

			return "Other:" + o;
		}

		bool WillAssemblyBeModified(AssemblyDefinition assembly)
		{
			// TODO by Mike : Should other values be considered true?
			return _context.Annotations.GetAction(assembly) == AssemblyAction.Link;
		}

		private bool TryPushAsKnownType(object o)
		{
			if (o is TypeDefinition t)
			{
				Push(t);
				return true;
			}

			if (o is IMemberDefinition m)
			{
				Push(m);
				return true;
			}

			if (o is TypeReference typeRef)
			{
				Push(typeRef);
				return true;
			}

			if (o is MemberReference mRef)
			{
				Push(mRef);
				return true;
			}

			if (o is ModuleDefinition module)
			{
				Push(module);
				return true;
			}

			return false;
		}

		bool ShouldRecordDependencyInformation(object o)
		{
			if (o is TypeDefinition t)
			{
				return WillAssemblyBeModified(t.Module.Assembly);
			}

			if (o is IMemberDefinition m)
			{
				return WillAssemblyBeModified(m.DeclaringType.Module.Assembly);
			}

			if (o is TypeReference typeRef)
			{
				return WillAssemblyBeModified(typeRef.Resolve().Module.Assembly);
			}

			if (o is MemberReference mRef)
			{
				return WillAssemblyBeModified(mRef.Resolve().DeclaringType.Module.Assembly);
			}

			if (o is ModuleDefinition module)
			{
				return WillAssemblyBeModified(module.Assembly);
			}

			return true;
		}

		class DependencyScope : IDisposable
		{
			private readonly Dependencies _parent;

			public DependencyScope(Dependencies parent)
			{
				_parent = parent;
			}

			public void Dispose()
			{
				_parent.Pop();
			}
		}
	}
}
