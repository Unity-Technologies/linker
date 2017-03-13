using Mono.Linker.Tests.Core;
using Mono.Linker.Tests.CoreIntegration;
using Mono.Linker.Tests.NUnitIntegration;
using NUnit.Framework;

namespace Mono.Linker.Tests
{
    [TestFixture]
    public class CommonTests
    {
        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.AllTests))]
        public void AllTests(TestCase testCase)
        {
            var runner = new TestRunner(new ObjectFactory());
            runner.Run(testCase);
        }
    }
}
