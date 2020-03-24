using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.GetMethod
{
    public class FromGetType
    {
        public static void Main()
        {
            new Foo().Run();
        }

        [Kept]
        [KeptMember(".ctor()")]
        class Foo
        {
            [Kept]
            public void Run()
            {
                var m = GetType().GetMethod("TakingStringReturningInt", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            [Kept]
            private void TakingStringReturningInt(int s)
            {
            }
        }
    }
}
