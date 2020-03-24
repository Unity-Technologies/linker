using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructors
{
    public class WithMembersInBase
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetConstructors())
                Console.WriteLine(m);
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Foo : Base
        {
            [Kept]
            public Foo()
            {
            }
        }

        [Kept]
        class Base
        {
            [Kept] // Kept because of Foo's ctor(), not because of reflection usage
            public Base()
            {
            }

            public Base(int arg)
            {
            }
        }
    }
}
