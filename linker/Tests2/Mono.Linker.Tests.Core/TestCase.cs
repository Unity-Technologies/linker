using System;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class TestCase
    {
        public TestCase(NPath sourceFile, string name, NPath rootCasesDirectory)
        {
            SourceFile = sourceFile;
            Name = name;

            // A little hacky, but good enough for name.  No reason why namespace & type names
            // should not follow the directory structure
            FullTypeName = $"{sourceFile.Parent.RelativeTo(rootCasesDirectory.Parent).ToString(SlashMode.Forward).Replace('/', '.')}.{sourceFile.FileNameWithoutExtension}";
        }

        public string Name { get; }

        public NPath SourceFile { get; }

        public string FullTypeName { get; }

        public bool HasLinkXmlFile
        {
            get { return SourceFile.ChangeExtension("xml").FileExists(); }
        }

        public NPath LinkXmlFile
        {
            get
            {
                if (!HasLinkXmlFile)
                    throw new InvalidOperationException("This test case does not have a link xml file");

                return SourceFile.ChangeExtension("xml");
            }
        }
    }
}
