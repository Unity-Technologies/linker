using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    [SetupCompileArgument("/optimize+")]
    public class GetMethodUsingName
    {
        public static void Main()
        {
            var b = typeof(Bar);
            b.GetMethod("Foo");
        }

        [Kept]
        class Bar
        {
            [Kept]
            public static void Foo()
            {
            }

            public static void FooRemove()
            {
            }
        }
    }
}
