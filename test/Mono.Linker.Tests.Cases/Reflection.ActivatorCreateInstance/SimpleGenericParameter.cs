using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.ActivatorCreateInstance
{
    public class SimpleGenericParameter
    {
        public static void Main()
        {
            var f = Activator.CreateInstance<Foo>();
        }

        [Kept]
        [KeptMember(".ctor()")]
        class Foo
        {
        }
    }
}
