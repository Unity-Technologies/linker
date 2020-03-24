using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.IsAssignableFrom
{
    [SetupCompileArgument("/optimize+")]
    public class BaseAndInterfaces
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
            var ibaseType = typeof(IBase);
            var baseType = typeof(Base);
            var derivedType = typeof(Derived);

            if (ibaseType.IsAssignableFrom(baseType))
                value++;
            if (ibaseType.IsAssignableFrom(derivedType))
                value++;
            if (!baseType.IsAssignableFrom(ibaseType))
                value++;
            if (!derivedType.IsAssignableFrom(ibaseType))
                value++;

            return value;
        }

        [Kept]
        interface IBase
        {
        }

        [Kept]
        [KeptInterface(typeof(IBase))]
        class Base : IBase
        {
        }

        [Kept]
        [KeptBaseType(typeof(Base))]
        class Derived : Base
        {
        }
    }
}
