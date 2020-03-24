using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetFields
{
    public class WithMembersInBase
    {
        public static void Main()
        {
            foreach (var f in typeof(Foo).GetFields())
                Console.WriteLine(f.Name);
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Foo : Base
        {
            [Kept]
            public int Field1;
        }

        [Kept]
        class Base
        {
            [Kept]
            public int BaseField1;
        }
    }
}
