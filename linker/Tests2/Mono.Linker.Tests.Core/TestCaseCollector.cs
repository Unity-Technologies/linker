using System.Collections.Generic;
using System.IO;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class TestCaseCollector
    {
        private readonly NPath _rootDirectory;

        public TestCaseCollector(string rootDirectory)
            : this(rootDirectory.ToNPath())
        {
        }

        public TestCaseCollector(NPath rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        public IEnumerable<TestCase> Collect()
        {
            _rootDirectory.DirectoryMustExist();

            // TODO by Mike : Assert Main() exists
            // TODO by Mike : Skip NotATestCase

            foreach (var file in _rootDirectory.Files("*.cs"))
                yield return new TestCase(file, FormatTestCaseName(file), _rootDirectory);

            foreach (var subDir in _rootDirectory.Directories())
            {
                if (subDir.FileName == "bin" || subDir.FileName == "obj" || subDir.FileName == "Properties")
                    continue;

                foreach (var file in subDir.Files("*.cs", true))
                    yield return new TestCase(file, FormatTestCaseName(file), _rootDirectory);
            }
        }

        private string FormatTestCaseName(NPath sourceFilePath)
        {
            return $"{sourceFilePath.RelativeTo(_rootDirectory).Parent.ToString(SlashMode.Forward).Replace('/', '.')}.{sourceFilePath.FileNameWithoutExtension}";
        }
    }
}
