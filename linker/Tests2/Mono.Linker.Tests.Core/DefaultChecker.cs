using System;
using System.Linq;
using Mono.Cecil;
using Mono.Linker.Tests.Core.Base;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class DefaultChecker : BaseChecker
    {
        public DefaultChecker(TestCase testCase, BaseAssertions assertions)
            : base(testCase, assertions)
        {
        }

        public override void Check(LinkedTestCaseResult linkResult)
        {
            Assert.IsTrue(linkResult.LinkedAssemblyPath.FileExists(), $"The linked output assembly was not found.  Expected at {linkResult.LinkedAssemblyPath}");
            using (var original = AssemblyDefinition.ReadAssembly(linkResult.InputAssemblyPath.ToString()))
            {
                using (var linked = AssemblyDefinition.ReadAssembly(linkResult.LinkedAssemblyPath.ToString()))
                {
                    CompareAssemblies(original, linked);
                }
            }
        }

        protected virtual void CompareAssemblies(AssemblyDefinition original, AssemblyDefinition linked)
        {
            foreach (TypeDefinition originalType in original.MainModule.AllTypes())
            {
                TypeDefinition linkedType = linked.MainModule.GetType(originalType.FullName);

                CheckDefinition(originalType, linkedType, "Type");

                // TODO by Mike : Fix: SHould not continue just because the linked type is gone.  What if the linked type was mistakenly
                // removed and there are still methods/fields on that type that we should be asserting

                if (linkedType == null)
                    continue;

                CompareTypes(originalType, linkedType);
            }
        }

        protected void CheckDefinition(IMemberDefinition original, IMemberDefinition linked, string definitionTypeName)
        {
            if (original.ShouldBeRemoved())
            {
                Assert.IsNull(linked, $"{definitionTypeName} `{original}' should not have been removed");
                return;
            }

            if (original.ShouldBeKept())
            {
                Assert.IsNotNull(linked, $"{definitionTypeName} `{original}' should have been kept");
            }
        }

        protected void CompareTypes(TypeDefinition type, TypeDefinition linkedType)
        {
            foreach (FieldDefinition originalField in type.Fields)
            {
                FieldDefinition linkedField = linkedType.Fields.FirstOrDefault(f => f.FullName == originalField.FullName);
                CheckDefinition(originalField, linkedField, "Field");
            }

            foreach (MethodDefinition originalMethod in type.Methods)
            {
                MethodDefinition linkedMethod = linkedType.Methods.FirstOrDefault(m => m.GetFullName() == originalMethod.GetFullName());
                CheckDefinition(originalMethod, linkedMethod, "Method");
            }
        }
    }
}
