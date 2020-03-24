using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.CreateDelegate
{
    public class StaticSimple
    {
        public static void Main()
        {
            var d = (FooDel)Delegate.CreateDelegate(typeof(FooDel), typeof(StaticSimple), "TestMethod");
        }

        [Kept]
        static void TestMethod()
        {
        }

        [Kept]
        [KeptBaseType(typeof(MulticastDelegate))]
        [KeptMember("Invoke()")]
        [KeptMember("BeginInvoke(System.AsyncCallback,System.Object)")]
        [KeptMember("EndInvoke(System.IAsyncResult)")]
        [KeptMember(".ctor(System.Object,System.IntPtr)")]
        private delegate void FooDel();
    }
}
