using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructors
{
    public class NonPublicAreStillRemoved
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetConstructors())
                Console.WriteLine(m);
        }

        [Kept]
        class Foo
        {
            internal Foo()
            {
            }

            private Foo(int arg)
            {
            }

            protected Foo(string arg)
            {
            }
        }
    }
}
