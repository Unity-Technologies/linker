using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.VirtualMethods
{
    class HarderToDetectUnusedVirtualMethodGetsRemoved
    {
        public static void Main()
        {
            new Base().Call();
        }

        static void DeadCode()
        {
            new B();
        }

        class Base
        {
            [Kept]
            public virtual void Call() { }
        }
        class B : Base
        {
            [Removed]
            public override void Call() { }
       }
    }
}
