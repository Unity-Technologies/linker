using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    public class InstanceBindingDoesNotCatchStaticMethod
    {
        public static void Main()
        {
            typeof(Foo<>).MakeGenericType(typeof(Bar)).GetMethod("Blah", BindingFlags.Instance | BindingFlags.Public);
        }

        [Kept]
        class Bar
        {
        }

        [Kept]
        class Foo<T>
        {
            public static void Blah()
            {
            }
        }
    }
}
