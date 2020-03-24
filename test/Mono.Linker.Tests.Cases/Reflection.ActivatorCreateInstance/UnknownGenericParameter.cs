using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.ActivatorCreateInstance
{
    public class UnknownGenericParameter
    {
        public static void Main()
        {
            var f = Helper<Foo>();
        }

        [Kept]
        static T Helper<T>()
        {
            return Activator.CreateInstance<T>();
        }

        [Kept]
        class Foo
        {
        }
    }
}
