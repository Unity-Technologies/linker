using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    [SetupCompileArgument("/optimize+")]
    public class WithDup
    {
        public static void Main()
        {
            var b = typeof(Bar);
            b.GetMethod("Blah1", BindingFlags.Static | BindingFlags.Public);
            b.GetMethod("Blah2", BindingFlags.Static | BindingFlags.Public);
            b.GetMethod("Blah3", BindingFlags.Static | BindingFlags.Public);
            b.GetMethod("Blah4", BindingFlags.Static | BindingFlags.Public);
        }

        [Kept]
        class Bar
        {
            [Kept]
            public static void Blah1()
            {
            }

            [Kept]
            public static void Blah2()
            {
            }

            [Kept]
            public static void Blah3()
            {
            }

            [Kept]
            public static void Blah4()
            {
            }
        }
    }
}
