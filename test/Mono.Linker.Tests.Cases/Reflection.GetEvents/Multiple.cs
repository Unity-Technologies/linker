using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetEvents
{
    public class Multiple
    {
        public static void Main()
        {
            // Create an instance to avoid unreachable bodies, which makes asserting harder to define
            new Foo();

            foreach (var f in typeof(Foo).GetEvents())
                Console.WriteLine(f.Name);
        }

        [Kept]
        [KeptMember(".ctor()")]
        class Foo
        {
            [Kept]
            [KeptBackingField]
            [KeptEventAddMethod]
            [KeptEventRemoveMethod]
            public event EventHandler Event1;

            [Kept]
            [KeptBackingField]
            [KeptEventAddMethod]
            [KeptEventRemoveMethod]
            public static event EventHandler StaticEvent1;
        }
    }
}
