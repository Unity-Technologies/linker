using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethods
{
    public class NonPublicAreStillRemoved
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetMethods())
                Console.WriteLine(m);
        }

        [Kept]
        class Foo
        {
            internal void Method1()
            {
            }

            private void Method2()
            {
            }

            protected void Method3()
            {
            }

            internal static void StaticMethod1()
            {
            }

            private static void StaticMethod2()
            {
            }
        }
    }
}
