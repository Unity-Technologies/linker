using System;
using System.Reflection;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetConstructor
{
    [SetupCompileArgument("/optimize+")]
    public class GetConstructorUsingTypeArray
    {
        public static void Main()
        {
            var b = typeof(Bar);
            b.GetConstructor(new Type[] {});
            b.GetConstructor(new[] { typeof(int) });
            b.GetConstructor(new[] { typeof(float[]) });

            b.GetConstructor(BindingFlags.NonPublic, null, new[] { typeof(int), typeof(int) }, null);
        }

        [Kept]
        class Bar
        {
            [Kept]
            public Bar()
            {
            }

            [Kept]
            public Bar(int val)
            {
            }

            [Kept]
            public Bar(float[] vals)
            {
            }

            public Bar(float val)
            {
            }

            [Kept]
            Bar(int i, int j)
            {
            }

            Bar(float i, float j)
            {
            }
        }
    }
}
