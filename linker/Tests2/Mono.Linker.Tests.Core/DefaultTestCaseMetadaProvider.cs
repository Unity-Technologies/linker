using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
