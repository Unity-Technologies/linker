using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetField
{
    public class GetFieldInBaseClassFromDerivedClass
    {
        public static void Main()
        {
            var t = typeof(Derived<int>);
            t.GetField("Field");
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
            public int Field;
        }
    }
}
