using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetFields
{
    public class Multiple
    {
        public static void Main()
        {
            foreach (var f in typeof(Foo).GetFields())
                Console.WriteLine(f.Name);
        }

        [Kept]
        class Foo
        {
            [Kept]
            public int Field1;

            [Kept]
            public static int StaticField1;
        }
    }
}
