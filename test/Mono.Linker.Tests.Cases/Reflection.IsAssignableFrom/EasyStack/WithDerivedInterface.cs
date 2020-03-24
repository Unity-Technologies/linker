using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.IsAssignableFrom.EasyStack
{
    [IgnoreTestCase("TODO by Mike : Need to implement support for this")]
    [SetupCompileArgument("/optimize+")]
    public class WithDerivedInterface
    {
        public static void Main()
        {
            Run();
        }

        [Kept]
        // An ability our test framework supports.  I can bring it upstream if you'd like
        // The original motivation for this attribute was our body editing code.  However, it is useful for reflection tests such as this one
        // as a verification that your test gives the same results once reflection is handled
        //[CheckInvokeMatchesOriginal]
        static int Run()
        {
            int value = 0;

            if (typeof(IBase).IsAssignableFrom(typeof(Base)))
                value++;

            return value;
        }

        [Kept]
        interface IBase
        {
        }

        [Kept]
        [KeptInterface(typeof(IBase))]
        interface IBase2 : IBase
        {
        }

        [Kept]
        [KeptInterface(typeof(IBase2))]
        interface IBase3 : IBase2
        {
        }

        [Kept]
        [KeptInterface(typeof(IBase3))]
        class Base : IBase3
        {
        }
    }
}
