using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    [SetupCompileArgument("/optimize+")]
    public class WithDupAndGeneric
    {
        public static void Main()
        {
            var f = typeof(Foo<>);
            f.MakeGenericType(typeof(Bar)).GetMethod("Blah1", BindingFlags.Static | BindingFlags.Public);
            f.MakeGenericType(typeof(Bar)).GetMethod("Blah2", BindingFlags.Static | BindingFlags.Public);
            f.MakeGenericType(typeof(Bar)).GetMethod("Blah3", BindingFlags.Static | BindingFlags.Public);
            f.MakeGenericType(typeof(Bar)).GetMethod("Blah4", BindingFlags.Static | BindingFlags.Public);
        }

        [Kept]
        class Bar
        {
        }

        [Kept]
        class Foo<T>
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
