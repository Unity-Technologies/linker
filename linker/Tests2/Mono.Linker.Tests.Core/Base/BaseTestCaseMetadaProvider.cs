using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core.Base
{
    public abstract class BaseTestCaseMetadaProvider
    {
        protected readonly TestCase _testCase;

        protected BaseTestCaseMetadaProvider(TestCase testCase)
        {
            _testCase = testCase;
        }
        // TODO by Mike : Doesn't feel like the best home for this...
        public abstract NPath ProfileDirectory { get; }

        public abstract IEnumerable<string> GetReferencedAssemblies();

        public abstract TestCaseLinkerOptions GetLinkerOptions();
    }
}
