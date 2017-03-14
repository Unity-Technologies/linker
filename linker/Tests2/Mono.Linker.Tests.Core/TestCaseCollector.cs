using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Linker.Tests.Cases.Expectations;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class TestCaseCollector
    {
        private readonly NPath _rootDirectory;
        private readonly NPath _testCaseAssemblyPath;

        public TestCaseCollector(string rootDirectory, string testCaseAssemblyPath)
            : this(rootDirectory.ToNPath(), testCaseAssemblyPath.ToNPath())
        {
        }

        public TestCaseCollector(NPath rootDirectory, NPath testCaseAssemblyPath)
        {
            _rootDirectory = rootDirectory;
            _testCaseAssemblyPath = testCaseAssemblyPath;
        }

        public IEnumerable<TestCase> Collect()
        {
            _rootDirectory.DirectoryMustExist();
            _testCaseAssemblyPath.FileMustExist();

            // TODO by Mike : Assert Main() exists
            // TODO by Mike : Skip NotATestCase

            {

            }
        }

        {
        }
    }
}
