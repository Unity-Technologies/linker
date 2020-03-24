using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetField
{
    public class GetFieldInGenericClass
    {
        public static void Main()
        {
            var t = typeof(Foo<int>);
            t.GetField("Field");
        }

        [Kept]
        class Foo<T>
        {
            [Kept]
            public int Field;
        }
    }
}
