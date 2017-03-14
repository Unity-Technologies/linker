using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Basic
{
    class UninvokedInterfaceMemberGetsRemoved
    {
        public static void Main() { new B(); }

        interface I
        {
            [Removed]
            void Method();
        }

        class B : I
        {
            [Removed]
            public void Method() { }
        }
    }
}
