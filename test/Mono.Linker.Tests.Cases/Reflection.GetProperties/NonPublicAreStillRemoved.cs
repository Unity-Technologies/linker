using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperties
{
    public class NonPublicAreStillRemoved
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetProperties())
                Console.WriteLine(m.Name);
        }

        [Kept]
        class Foo
        {
            internal int Property1 { get; set; }
            private int Property2 { get; set; }
            protected int Property3 { get; set; }

            internal static int StaticProperty1 { get; set; }
            private static int StaticProperty2 { get; set; }
        }
    }
}
