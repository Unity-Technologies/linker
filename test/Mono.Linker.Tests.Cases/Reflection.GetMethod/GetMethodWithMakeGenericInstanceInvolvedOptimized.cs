using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    [SetupCompileArgument("/optimize+")]
    public class GetMethodWithMakeGenericInstanceInvolvedOptimized
    {
        public static void Main()
        {
            typeof(Foo<>).MakeGenericType(typeof(Bar)).GetMethod("Blah", BindingFlags.Static | BindingFlags.Public);
        }

        [Kept]
        class Bar
        {
        }

        [Kept]
        class Foo<T>
        {
            [Kept]
            public static void Blah()
            {
            }
        }
    }
}
