using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperty
{
    public class GetIndexerPropertyUsingTypeArray
    {
        public static void Main()
        {
            new Foo().GetType().GetProperty("Item", new[] { typeof(int), typeof(int) });
        }

        [Kept]
        [KeptAttributeAttribute("System.Reflection.DefaultMemberAttribute")]
        [KeptMember(".ctor()")]
        class Foo
        {
            [Kept]
            public int this[int i, int j]
            {
                [Kept]
                get { return 42; }
            }
        }
    }
}
