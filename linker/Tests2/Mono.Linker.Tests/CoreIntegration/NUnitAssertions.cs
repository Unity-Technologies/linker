using Mono.Linker.Tests.Core.Base;
using NUnit.Framework;

namespace Mono.Linker.Tests.CoreIntegration
{
    public class NUnitAssertions : BaseAssertions
    {
        public override void IsNull(object obj, string message)
        {
            Assert.IsNull(obj, message);
        }

        public override void IsNotNull(object obj, string message)
        {
            Assert.IsNotNull(obj, message);
        }

        public override void IsTrue(bool value, string message)
        {
            Assert.IsTrue(value, message);
        }
    }
}
