using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Core.Base;
using Mono.Linker.Tests.Core.Utils;

namespace Mono.Linker.Tests.Core
{
    public class DefaultChecker : BaseChecker
    {
        private readonly BaseAssertions _realAssertions;
        private readonly AssertionCounter _assertionCounter;

        public DefaultChecker(TestCase testCase, BaseAssertions assertions)
            : base(testCase, new AssertionCounter(assertions))
        {
            _realAssertions = assertions;
            _assertionCounter = (AssertionCounter)Assert;
        }

        protected BaseAssertions AssertNonCounted => _realAssertions;

        protected void BumpAssertionCounter()
        {
            _assertionCounter.Bump();
        }

        public override void Check(LinkedTestCaseResult linkResult)
        {
            AssertNonCounted.IsTrue(linkResult.LinkedAssemblyPath.FileExists(), $"The linked output assembly was not found.  Expected at {linkResult.LinkedAssemblyPath}");
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
            var membersToAssert = original.MainModule.AllMembers().Where(m => m.HasAttributeDerivedFrom(nameof(BaseExpectedLinkedBehaviorAttribute))).ToArray();
            foreach (var originalMember in membersToAssert)
            {
                if (originalMember is TypeDefinition)
                {
                    TypeDefinition linkedType = linked.MainModule.GetType(originalMember.FullName);
                    CheckTypeDefinition((TypeDefinition)originalMember, linkedType);
                }
                else if (originalMember is FieldDefinition)
                {
                    TypeDefinition linkedType = linked.MainModule.GetType(originalMember.DeclaringType.FullName);
                    CheckTypeMember(originalMember, linkedType, "Field", () => linkedType.Fields, f => f.FullName);
                }
                else if (originalMember is MethodDefinition)
                {
                    var originalMethodDef = (MethodDefinition) originalMember;
                    TypeDefinition linkedType = linked.MainModule.GetType(originalMember.DeclaringType.FullName);
                    CheckTypeMember(originalMethodDef, linkedType, "Method", () => linkedType.Methods, m => m.GetFullName());
                }
                else
                {
                    throw new NotImplementedException($"Don't know how to check member of type {originalMember.GetType()}");
                }
            }

            // This is a safety check to help reduce false positive passes.  A test could pass if there was a bug in the checking logic that never made an assert.  This check is here
            // to make sure we make the number of assertions that we expect
            AssertNonCounted.AreEqual(_assertionCounter.AssertionsMade, membersToAssert.Length, $"Expected to make {membersToAssert.Length} assertions, but only made {_assertionCounter.AssertionsMade}.  The test may be invalid or there may be a bug in the checking logic");
        }

        protected virtual void CheckTypeMember<T>(T originalMember, TypeDefinition linkedParentTypeDefinition, string definitionTypeName, Func<IEnumerable<T>> getLinkedMembers, Func<T, string> getName) where T : IMemberDefinition
        {
            if (originalMember.ShouldBeRemoved())
            {
                if (linkedParentTypeDefinition == null)
                {
                    // The entire type was removed, which means the field or method was also removed.  This is OK.
                    // We don't have anything we can assert in this case.
                    BumpAssertionCounter();
                }
                else
                {
                    var originalName = getName(originalMember);
                    var linkedMember = getLinkedMembers().FirstOrDefault(linked => getName(linked) == originalName);
                    Assert.IsNull(linkedMember, $"{definitionTypeName}: `{originalMember}' should have been removed");
                }

                return;
            }

            if (originalMember.ShouldBeKept())
            {
                // if the member should be kept, then there's an implied requirement that the parent type exists.  Let's make that check
                // even if the test case didn't request it otherwise we are just going to hit a null reference exception when we try to get the members on the type
                _realAssertions.IsNotNull(linkedParentTypeDefinition, $"{definitionTypeName}: `{originalMember}' should have been kept, but the entire parent type was removed {originalMember.DeclaringType}");

                var originalName = getName(originalMember);
                var linkedMember = getLinkedMembers().FirstOrDefault(linked => getName(linked) == originalName);
                Assert.IsNotNull(linkedMember, $"{definitionTypeName}: `{originalMember}' should have been kept");
            }
        }

        protected virtual void CheckTypeDefinition(TypeDefinition original, TypeDefinition linked)
        {
            if (original.ShouldBeRemoved())
            {
                Assert.IsNull(linked, $"Type: `{original}' should have been removed");
                return;
            }

            if (original.ShouldBeKept())
            {
                Assert.IsNotNull(linked, $"Type: `{original}' should have been kept");
            }
        }

        private class AssertionCounter : BaseAssertions
        {
            private readonly BaseAssertions _realAssertions;

            public AssertionCounter(BaseAssertions realAssertions)
            {
                _realAssertions = realAssertions;
            }

            public int AssertionsMade { get; private set; }

            public void Bump()
            {
                AssertionsMade++;
            }

            public override void IsNull(object obj, string message)
            {
                Bump();
                _realAssertions.IsNull(obj, message);
            }

            public override void IsNotNull(object obj, string message)
            {
                Bump();
                _realAssertions.IsNotNull(obj, message);
            }

            public override void IsTrue(bool value, string message)
            {
                Bump();
                _realAssertions.IsTrue(value, message);
            }

            public override void Ignore(string reason)
            {
                throw new NotSupportedException();
            }

            public override void AreEqual(object expected, object actual, string message)
            {
                throw new NotSupportedException();
            }
        }
    }
}
