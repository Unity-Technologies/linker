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
                var testsAssemblyPath = new Uri(typeof(TestDatabase).Assembly.CodeBase).LocalPath.ToNPath();
                return testsAssemblyPath.Parent.Parent.Parent.Parent.Combine("Mono.Linker.Tests.Cases").DirectoryMustExist();
            }
        }

        private static NPath TestCaseAssemblyPath
        {
            get
            {
                // TODO by Mike : Clean up path finding by referencing the assembly?
                return RootTestCaseDirectory.Combine("bin", "Debug", "Mono.Linker.Tests.Cases.dll");
            }
        }

        public IEnumerable AllTests()
        {
            return AllTestCases(RootTestCaseDirectory);
        }

        public static IEnumerable AllTestCases(NPath rootTestCaseDirectory)
        {
            var testCases = new TestCaseCollector(rootTestCaseDirectory, TestCaseAssemblyPath);
            return MakeTestCasesForProfile(testCases.Collect().ToArray());
        }

        private static IEnumerable<TestCaseData> MakeTestCasesForProfile(TestCase[] testCases)
        {
            foreach (var test in testCases.OrderBy(t => t.DisplayName))
            {
                var data = new TestCaseData(test);
                data.SetName(test.DisplayName);
                yield return data;
            }
        }
    }
}
