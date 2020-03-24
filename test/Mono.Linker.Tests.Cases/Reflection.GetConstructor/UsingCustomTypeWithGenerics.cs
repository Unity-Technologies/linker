using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructor
{
    public class UsingCustomTypeWithGenerics
    {
        public static void Main()
        {
            var b = typeof(Foo);
            b.GetConstructor(new[] { typeof(Generic<A>) });
        }

        [Kept]
        class Foo
        {
            [Kept]
            public Foo(Generic<A> a)
            {
            }

            public Foo(Generic<B> b)
            {
            }

            public Foo(Generic<C> c)
            {
            }

            public Foo(A a)
            {
            }

            public Foo(B b)
            {
            }

            public Foo(C c)
            {
            }
        }

        [Kept]
        class Generic<T>
        {
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
