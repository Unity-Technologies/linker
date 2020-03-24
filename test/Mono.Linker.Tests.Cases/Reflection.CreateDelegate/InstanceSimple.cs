using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.CreateDelegate
{
    public class InstanceSimple
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
                var d = (FooDel)Delegate.CreateDelegate(typeof(FooDel), this, "TestMethod");
            }

            [Kept]
            void TestMethod()
            {
            }
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
