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
