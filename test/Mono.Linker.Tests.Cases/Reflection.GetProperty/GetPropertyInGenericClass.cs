using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperty
{
    public class GetPropertyInGenericClass
    {
        public static void Main()
        {
            var t = typeof(Foo<int>);
            t.GetProperty("Property");
        }

        [Kept]
        class Foo<T>
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
