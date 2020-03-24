using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetField
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
            public int Field1;
        }

        [Kept]
        [KeptMember(".ctor()")]
        class ObjectParameter<T>
        {
            [Kept]
            public void UseReflectionMethod(T value)
            {
                value.GetType().GetField(nameof(Foo.Field1));
            }
        }
    }
}
