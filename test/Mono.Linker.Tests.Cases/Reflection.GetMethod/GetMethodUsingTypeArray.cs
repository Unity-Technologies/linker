using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    [SetupCompileArgument("/optimize+")]
    public class GetMethodUsingTypeArray
    {
        public static void Main()
        {
            var b = typeof(Bar);
            b.GetMethod("Foo", new[] { typeof(int) });
            b.GetMethod("Foo", new[] { typeof(int), typeof(float) });
            b.GetMethod("Foo", new[] { typeof(int[]) });
            b.GetMethod("FooReturn", new[] { typeof(int) });
            b.GetMethod("FooReturn", new[] { typeof(float), typeof(int) });
        }

        [Kept]
        class Bar
        {
            [Kept]
            public static void Foo(int val)
            {
            }

            [Kept]
            public static void Foo(int i, float f)
            {
            }

            [Kept]
            public static void Foo(int[] vals)
            {
            }

            public static void Foo(float val)
            {
            }

            [Kept]
            public static int FooReturn(int val)
            {
                return 42;
            }

            [Kept]
            public static int FooReturn(float f, int i)
            {
                return 42;
            }

            public static int FooReturn(float val)
            {
                return 42;
            }
        }
    }
}
