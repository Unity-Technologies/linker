using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethods
{
    public class Multiple
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetMethods())
                Console.WriteLine(m);
        }

        [Kept]
        class Foo
        {
            [Kept]
            public void Method1()
            {
            }

            [Kept]
            public static void StaticMethod1()
            {
            }
        }
    }
}
