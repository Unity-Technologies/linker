using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetEvent
{
    public class GetEventInBaseClassFromDerivedClass
    {
        public static void Main()
        {
            var t = typeof(Derived<int>);
            t.GetEvent("Event");
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Derived<T> : Base
        {
        }

        [Kept]
        class Base
        {
            [Kept]
            [KeptBackingField]
            [KeptEventAddMethod]
            [KeptEventRemoveMethod]
            public static event EventHandler Event;
        }
    }
}
