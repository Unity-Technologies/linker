using System.Runtime.InteropServices;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Interop
{
    class UnusedTypeWithSequentialLayoutIsRemoved
    {
        static void Main() { }

        [Removed]
        [StructLayout(LayoutKind.Sequential)]
        class B
        {
            int a;
        }
    }
}
