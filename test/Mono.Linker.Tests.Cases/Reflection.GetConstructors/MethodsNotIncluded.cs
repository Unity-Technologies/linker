using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructors
{
    public class MethodsNotIncluded
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetConstructors())
                Console.WriteLine(m);
        }

        [Kept]
        class Foo
        {
            [Kept]
            public Foo()
            {
            }

            public void Method1()
            {
            }
        }
    }
}
