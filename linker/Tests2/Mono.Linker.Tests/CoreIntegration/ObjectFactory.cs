using System;
using Mono.Linker.Tests.Core;
using Mono.Linker.Tests.Core.Base;

namespace Mono.Linker.Tests.CoreIntegration
{
    public class ObjectFactory : BaseObjectFactory
    {
        public override BaseLinker CreateLinker(TestCase testCase)
        {
            return new MonoLinker(testCase);
        }

        public override BaseAssertions CreateAssertions()
        {
            return new NUnitAssertions();
        }
    }
}
