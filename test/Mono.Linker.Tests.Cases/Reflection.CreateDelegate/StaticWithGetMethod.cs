using System;
using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.CreateDelegate
{
    public class StaticWithGetMethod
    {
        public static void Main()
        {
            var d =
                (DelegateTakingStringReturningInt)
                Delegate.CreateDelegate(typeof(DelegateTakingStringReturningInt), null,
                    typeof(StaticWithGetMethod).GetMethod("TakingStringReturningInt", BindingFlags.Static | BindingFlags.NonPublic));


            d(5);
        }

        [Kept]
        [KeptBaseType(typeof(MulticastDelegate))]
        [KeptMember("Invoke(System.Int32)")]
        [KeptMember("BeginInvoke(System.Int32,System.AsyncCallback,System.Object)")]
        [KeptMember("EndInvoke(System.IAsyncResult)")]
        [KeptMember(".ctor(System.Object,System.IntPtr)")]
        private delegate void DelegateTakingStringReturningInt(int s);

        [Kept]
        private static void TakingStringReturningInt(int s)
        {
        }
    }
}
