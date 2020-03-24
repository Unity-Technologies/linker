using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetEvent
{
    public class GetEventInGenericClass
    {
        public static void Main()
        {
            var t = typeof(Foo<int>);
            t.GetEvent("Event");
        }

        [Kept]
        class Foo<T>
        {
            [Kept]
            [KeptBackingField]
            [KeptEventAddMethod]
            [KeptEventRemoveMethod]
            public static event EventHandler Event;
        }
    }
}
