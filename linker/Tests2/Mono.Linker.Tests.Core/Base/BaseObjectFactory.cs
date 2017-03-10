namespace Mono.Linker.Tests.Core.Base
{
    public abstract class BaseObjectFactory
    {
        public virtual BaseTestSandbox CreateSandbox(TestCase testCase)
        {
            return new DefaultTestSandbox(testCase);
        }

        public abstract BaseCompiler CreateCompiler(TestCase testCase);

        public abstract BaseLinker CreateLinker(TestCase testCase);

        public virtual BaseChecker CreateChecker(TestCase testCase, BaseAssertions assertions)
        {
            return new DefaultChecker(testCase, assertions);
        }

        public virtual BaseTestCaseMetadaProvider CreateMetadatProvider(TestCase testCase)
        {
            return new DefaultTestCaseMetadaProvider(testCase);
        }

        public abstract BaseAssertions CreateAssertions();

        public virtual BaseLinkerArgumentBuilder CreateLinkerArgumentBuilder()
        {
            return new DefaultLinkerArgumentBuilder();
        }
    }
}
