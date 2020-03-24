using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetFields
{
    public class NonPublicAreStillRemoved
    {
        public static void Main()
        {
            foreach (var f in typeof(Foo).GetFields())
                Console.WriteLine(f.Name);
        }

        [Kept]
        class Foo
        {
            internal int Field2;
            private int Field3;
            protected int Field4;

            internal static int StaticField2;
            private static int StaticField3;
        }
    }
}
