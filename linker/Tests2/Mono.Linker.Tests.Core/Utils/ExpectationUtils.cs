﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Core.Utils
{
    public static class ExpectationUtils
    {
        public static bool ShouldBeRemoved(this ICustomAttributeProvider provider)
        {
            return provider.HasAttribute(nameof(RemovedAttribute));
        }

        public static bool ShouldBeKept(this ICustomAttributeProvider provider)
        {
            return provider.HasAttribute(nameof(KeptAttribute));
        }

        private static bool HasAttribute(this ICustomAttributeProvider provider, string name)
        {
            return provider.CustomAttributes.Any(ca => ca.Constructor.DeclaringType.Name == name);
        }
    }
}