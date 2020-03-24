using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    public class GetMethodWithMakeGenericInstanceInvolved
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
