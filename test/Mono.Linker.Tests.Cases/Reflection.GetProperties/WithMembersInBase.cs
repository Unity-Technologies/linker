using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperties
{
    public class WithMembersInBase
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetProperties())
                Console.WriteLine(m.Name);
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Foo : Base
        {
            [Kept]
            public int Property1 { [Kept][ExpectBodyModified] get; [Kept][ExpectBodyModified] set; }
        }

        [Kept]
        class Base
        {
            [Kept]
            public int BaseProperty1 { [Kept][ExpectBodyModified] get; [Kept][ExpectBodyModified] set; }
        }
    }
}
