using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetProperty
{
    public class GetPropertyUsingNameAndReturnType
    {
        public static void Main()
        {
            var t = typeof(Foo);
            t.GetProperty("IntProperty", typeof(int));
        }

        [Kept]
        class Foo
        {
            [Kept]
            public static int IntProperty
            {
                [Kept]
                get { return 42; }
            }

            public static float FloatProperty
            {
                get { return 42.0f; }
            }
        }
    }
}
