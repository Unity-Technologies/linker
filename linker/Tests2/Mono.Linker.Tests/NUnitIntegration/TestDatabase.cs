using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Linker.Tests.Core;
using Mono.Linker.Tests.Core.Utils;
using NUnit.Framework;

namespace Mono.Linker.Tests.NUnitIntegration
{
    public class TestDatabase
    {
        private static NPath RootTestCaseDirectory
        {
            get
            {
                var testsAssembly = new Uri(typeof(TestDatabase).Assembly.CodeBase).LocalPath.ToNPath();
                return testsAssembly.Parent.Parent.Parent.Parent.Combine("Mono.Linker.Tests.Cases").DirectoryMustExist();
            }
        }

        public IEnumerable AllTests()
        {
            return AllTestCases(RootTestCaseDirectory);
        }

        public static IEnumerable AllTestCases(NPath rootTestCaseDirectory)
        {
            var testCases = new TestCaseCollector(rootTestCaseDirectory.ToString());
            return MakeTestCasesForProfile(testCases.Collect().ToArray());
        }

        private static IEnumerable<TestCaseData> MakeTestCasesForProfile(TestCase[] testCases)
        {
            foreach (var test in testCases.OrderBy(t => t.Name))
            {
                var data = new TestCaseData(test);
                data.SetName(test.Name);
                yield return data;
            }
        }
    }
}
