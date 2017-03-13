using System;
using Mono.Linker.Tests.Core.Base;

namespace Mono.Linker.Tests.Core
{
    public class TestRunner
    {
        private readonly BaseObjectFactory _factory;

        public TestRunner(BaseObjectFactory factory)
        {
            _factory = factory;
        }

        public void Run(TestCase testCase)
        {
            var metadataProvider = _factory.CreateMetadatProvider(testCase);
            var sandbox = Sandbox(testCase);
            var compilationResult = Compile(testCase, sandbox, metadataProvider);
            PrepForLink(sandbox, compilationResult);
            var linkResult = Link(testCase, sandbox, compilationResult, metadataProvider);
            Check(testCase, linkResult);
        }

        private BaseTestSandbox Sandbox(TestCase testCase)
        {
            var sandbox = _factory.CreateSandbox(testCase);
            sandbox.Populate();
            return sandbox;
        }

        private ManagedCompilationResult Compile(TestCase testCase, BaseTestSandbox sandbox, BaseTestCaseMetadaProvider metadataProvider)
        {
            var compiler = _factory.CreateCompiler(testCase);
            return compiler.CompileTestIn(sandbox, metadataProvider.GetReferencedAssemblies());
        }

        private void PrepForLink(BaseTestSandbox sandbox, ManagedCompilationResult compilationResult)
        {
            var entryPointLinkXml = sandbox.InputDirectory.Combine("entrypoint.xml");
            LinkXmlHelpers.WriteXmlFileToPreserveEntryPoint(compilationResult.AssemblyPath, entryPointLinkXml);
        }

        private LinkedTestCaseResult Link(TestCase testCase, BaseTestSandbox sandbox, ManagedCompilationResult compilationResult, BaseTestCaseMetadaProvider metadataProvider)
        {
            var linker = _factory.CreateLinker(testCase);
            var builder = _factory.CreateLinkerArgumentBuilder();
            var caseDefinedOptions = metadataProvider.GetLinkerOptions();

            builder.AddOutputDirectory(sandbox.OutputDirectory);
            foreach(var linkXmlFile in sandbox.LinkXmlFiles)
                builder.AddLinkXmlFile(linkXmlFile);

            builder.AddSearchDirectory(sandbox.InputDirectory);
            builder.AddSearchDirectory(metadataProvider.ProfileDirectory);

            builder.AddCoreLink(caseDefinedOptions.CoreLink);

            linker.Link(builder.ToArgs());

            return new LinkedTestCaseResult { InputAssemblyPath = compilationResult.AssemblyPath, LinkedAssemblyPath = sandbox.OutputDirectory.Combine(compilationResult.AssemblyPath.FileName) };
        }

        private void Check(TestCase testCase, LinkedTestCaseResult linkResult)
        {
            var checker = _factory.CreateChecker(testCase, _factory.CreateAssertions());

            checker.Check(linkResult);
        }
    }
}
