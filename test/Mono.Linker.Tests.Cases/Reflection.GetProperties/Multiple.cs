using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperties
{
    public class Multiple
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetProperties())
                Console.WriteLine(m.Name);
        }

        [Kept]
        class Foo
        {
            [Kept]
            public int Property1 { [Kept][ExpectBodyModified] get; [Kept][ExpectBodyModified] set; }

            [Kept]
            [KeptBackingField]
            public static int StaticProperty1 { [Kept] get; [Kept] set; }
        }
    }
}
