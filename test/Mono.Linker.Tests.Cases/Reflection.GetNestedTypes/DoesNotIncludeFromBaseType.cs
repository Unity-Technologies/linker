using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetNestedTypes
{
    public class DoesNotIncludeFromBaseType
    {
        public static void Main()
        {
            foreach (var t in typeof(Foo).GetNestedTypes())
                Console.WriteLine(t.Name);
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Foo : Base
        {
            [Kept]
            public class A
            {
            }
        }

        [Kept]
        class Base
        {
            public class B
            {
            }

            public class C
            {
            }
        }
    }
}
