using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperty
{
    public class GetPropertyInBaseClassFromDerivedClass
    {
        public static void Main()
        {
            var t = typeof(Derived<int>);
            t.GetProperty("Property");
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
            public static int Property
            {
                [Kept]
                get { return 42; }
            }
        }
    }
}
