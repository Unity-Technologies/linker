using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethods
{
    public class WithMembersInBase
    {
        public static void Main()
        {
            foreach (var m in typeof(Foo).GetMethods())
                Console.WriteLine(m);
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Foo : Base
        {
            [Kept]
            public void Method1()
            {
            }
        }

        [Kept]
        class Base
        {
            [Kept]
            public void BaseMethod1()
            {
            }
        }
    }
}
