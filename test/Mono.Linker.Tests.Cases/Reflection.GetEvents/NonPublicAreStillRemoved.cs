using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetEvents
{
    public class NonPublicAreStillRemoved
    {
        public static void Main()
        {
            foreach (var f in typeof(Foo).GetEvents())
                Console.WriteLine(f.Name);
        }

        [Kept]
        class Foo
        {
            internal event EventHandler Event1;
            private event EventHandler Event2;
            protected event EventHandler Event3;

            internal static event EventHandler StaticEvent1;
            private static event EventHandler StaticEvent2;
        }
    }
}
