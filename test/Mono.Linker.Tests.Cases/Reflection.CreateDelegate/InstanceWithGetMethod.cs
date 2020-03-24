using System;
using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.CreateDelegate
{
    public class InstanceWithGetMethod
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
                var d =
                    (DelegateTakingStringReturningInt)
                    Delegate.CreateDelegate(typeof(DelegateTakingStringReturningInt), this,
                        GetType().GetMethod("TakingStringReturningInt", BindingFlags.Instance | BindingFlags.NonPublic));


                d(5);
            }

            [Kept]
            private void TakingStringReturningInt(int s)
            {
            }
        }

        [Kept]
        [KeptBaseType(typeof(MulticastDelegate))]
        [KeptMember("Invoke(System.Int32)")]
        [KeptMember("BeginInvoke(System.Int32,System.AsyncCallback,System.Object)")]
        [KeptMember("EndInvoke(System.IAsyncResult)")]
        [KeptMember(".ctor(System.Object,System.IntPtr)")]
        private delegate void DelegateTakingStringReturningInt(int s);
    }
}
