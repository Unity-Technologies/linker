using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Statics
{
    class UnusedStaticConstructorGetsRemoved
    {
        public static void Main() { }

        static void Dead() { new B(); }

        [Removed]
        class B
        {
            [Removed]
            static B() { }
        }
    }
}
