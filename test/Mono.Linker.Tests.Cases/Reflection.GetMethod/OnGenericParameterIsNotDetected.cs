using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    public class OnGenericParameterIsNotDetected
    {
        public static void Main()
        {
            var tmp = new ObjectParameter<Foo>();
            tmp.UseReflectionMethod(new Foo());
        }

        [Kept]
        [KeptMember(".ctor()")]
        class Foo
        {
            public void Method()
            {
            }
        }

        [Kept]
        [KeptMember(".ctor()")]
        class ObjectParameter<T>
        {
            [Kept]
            public void UseReflectionMethod(T value)
            {
                value.GetType().GetMethod(nameof(Foo.Method));
            }
        }
    }
}
