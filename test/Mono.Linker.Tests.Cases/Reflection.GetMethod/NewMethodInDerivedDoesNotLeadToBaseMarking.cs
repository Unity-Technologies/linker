using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    public class NewMethodInDerivedDoesNotLeadToBaseMarking
    {
        public static void Main()
        {
            var b = typeof(Bar);
            b.GetMethod("Method");
        }

        [Kept]
        class Base
        {
            public void Method()
            {
            }
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Bar : Base
        {
            [Kept]
            public new void Method()
            {
            }
        }
    }
}
