using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Mono.Linker.Tests.Core.Utils
{
    public static class CecilExtensions
    {
        public static IEnumerable<TypeDefinition> AllTypes(this ModuleDefinition module)
        {
            foreach (var type in module.Types)
            {
                yield return type;
                if (type.HasNestedTypes)
                {
                    foreach (var nestedType in type.NestedTypes)
                        yield return nestedType;
                }
            }
        }

        public static IEnumerable<IMemberDefinition> AllMembers(this ModuleDefinition module)
        {
            foreach (var type in module.AllTypes())
            {
                yield return type;

                foreach (var field in type.Fields)
                    yield return field;

                foreach (var method in type.Methods)
                    yield return method;
            }
        }

        public static bool HasAttribute(this ICustomAttributeProvider provider, string name)
        {
            return provider.CustomAttributes.Any(ca => ca.AttributeType.Name == name);
        }

        public static bool HasAttributeDerivedFrom(this ICustomAttributeProvider provider, string name)
        {
            return provider.CustomAttributes.Any(ca => ca.AttributeType.Resolve().DerivesFrom(name));
        }

        public static bool DerivesFrom(this TypeDefinition type, string baseTypeName)
        {
            if (type.BaseType == null)
                return false;

            if (type.BaseType.Name == baseTypeName)
                return true;

            return type.BaseType.Resolve().DerivesFrom(baseTypeName);
        }

        public static string GetFullName(this MethodReference method)
        {
            if (!method.HasGenericParameters)
                return method.FullName;

            var builder = new StringBuilder();
            builder.Append($"{method.ReturnType} {method.DeclaringType}::{method.Name}");
            builder.Append('<');

            for (int i = 0; i < method.GenericParameters.Count - 1; i++)
                builder.Append($"{method.GenericParameters[i]},");

            builder.Append($"{method.GenericParameters[method.GenericParameters.Count - 1]}>(");

            if (method.HasParameters)
            {
                for (int i = 0; i < method.Parameters.Count - 1; i++)
                    builder.Append($"{method.Parameters[i].ParameterType},");

                builder.Append(method.Parameters[method.Parameters.Count - 1].ParameterType);
            }

            builder.Append(")");

            return builder.ToString();
        }
    }
}
