using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructor
{
    public class UsingCustomTypeAsArrayElement
    {
        public static void Main()
        {
            var b = typeof(Foo);
            b.GetConstructor(new[] { typeof(A[]) });
        }

        [Kept]
        class Foo
        {
            [Kept]
            public Foo(A[] a)
            {
            }

            public Foo(B[] b)
            {
            }

            public Foo(C[] c)
            {
            }
        }

        [Kept]
        class A
        {
        }

        class B
        {
        }

        class C
        {
        }
    }
}
