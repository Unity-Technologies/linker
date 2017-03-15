using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Core.Base;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class DefaultTestCaseMetadaProvider : BaseTestCaseMetadaProvider
    {
        public DefaultTestCaseMetadaProvider(TestCase testCase)
            : base(testCase)
        {
        }

        public override TestCaseLinkerOptions GetLinkerOptions()
        {
            // This will end up becoming more complicated as we get into more complex test cases that require additional
            // data
            return new TestCaseLinkerOptions { CoreLink = "skip" };
        }

        public override NPath ProfileDirectory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<string> GetReferencedAssemblies()
        {
            yield return "mscorlib.dll";
        }

        public override IEnumerable<NPath> GetExtraLinkerSearchDirectories()
        {
            yield break;
        }

        public override bool IsIgnored(out string reason)
        {
            using (var def = AssemblyDefinition.ReadAssembly(_testCase.OriginalTestCaseAssemblyPath.ToString()))
            {
                var typeDef = def.MainModule.GetType(_testCase.FullTypeName);

                if (typeDef == null)
                    throw new InvalidOperationException($"Could not find the type definition for {_testCase.Name} in {_testCase.SourceFile}");

                if (typeDef.CustomAttributes.Any(ca => ca.Constructor.DeclaringType.Name == nameof(IgnoreTestCaseAttribute)))
                {
                    // TODO by Mike : Implement obtaining the real reason
                    reason = "TODO : Need to implement parsing reason";
                    return true;
                }
                else
                {
                    reason = null;
                    return false;
                }
            }
        }
    }
}
