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
			var membersToAssert = CollectMembersToAssert(original).ToArray();
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
					CheckTypeMember(originalMember, linkedType, "Field", () => linkedType.Fields);
				}
				else if (originalMember is MethodDefinition)
				{
					var originalMethodDef = (MethodDefinition) originalMember;
					TypeDefinition linkedType = linked.MainModule.GetType(originalMember.DeclaringType.FullName);
					CheckTypeMember(originalMethodDef, linkedType, "Method", () => linkedType.Methods);
				}
				else if (originalMember is PropertyDefinition)
				{
					// TODO by Mike : Need to implement.
					//  * Process the GetMethod().  It could have assert attributes.  Also need to respect an assert attribute on the PropertyDefinition
					//  * Process the SetMethod().  It could have assert attributes.  Also need to respect an assert attribute on the PropertyDefinition
					throw new NotImplementedException("Checking of properties has not been implemented yet");
				}
				else
				{
					throw new NotImplementedException($"Don't know how to check member of type {originalMember.GetType()}");
				}
			}

			// These are safety checks to help reduce false positive passes.  A test could pass if there was a bug in the checking logic that never made an assert.  This check is here
			// to make sure we make the number of assertions that we expect
			if (membersToAssert.Length == 0)
				_realAssertions.Fail($"Did not find any assertions to make.  Does the test case define any assertions to make?  Or there may be a bug in the collection of assertions to make");

			AssertNonCounted.AreEqual(_assertionCounter.AssertionsMade, membersToAssert.Length, $"Expected to make {membersToAssert.Length} assertions, but only made {_assertionCounter.AssertionsMade}.  The test may be invalid or there may be a bug in the checking logic");
		}

		protected virtual void CheckTypeMember<T>(T originalMember, TypeDefinition linkedParentTypeDefinition, string definitionTypeName, Func<IEnumerable<T>> getLinkedMembers) where T : IMemberDefinition
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
					var originalName = originalMember.GetFullName();
					var linkedMember = getLinkedMembers().FirstOrDefault(linked => linked.GetFullName() == originalName);
					Assert.IsNull(linkedMember, $"{definitionTypeName}: `{originalMember}' should have been removed");
				}

				return;
			}

			if (originalMember.ShouldBeKept())
			{
				// if the member should be kept, then there's an implied requirement that the parent type exists.  Let's make that check
				// even if the test case didn't request it otherwise we are just going to hit a null reference exception when we try to get the members on the type
				_realAssertions.IsNotNull(linkedParentTypeDefinition, $"{definitionTypeName}: `{originalMember}' should have been kept, but the entire parent type was removed {originalMember.DeclaringType}");

				var originalName = originalMember.GetFullName();
				var linkedMember = getLinkedMembers().FirstOrDefault(linked => linked.GetFullName() == originalName);
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

		private IEnumerable<IMemberDefinition> CollectMembersToAssert(AssemblyDefinition original)
		{
			var membersWithAssertAttributes = original.MainModule.AllMembers().Where(m => m.HasAttributeDerivedFrom(nameof(BaseExpectedLinkedBehaviorAttribute)));

			// Some of the assert attributes on classes flag methods that are not in the .cs for checking.  We need to collection the member definitions for these
			foreach (var member in membersWithAssertAttributes)
			{
				// For now, only support types of attributes on Types.
				var typeDefinition = member as TypeDefinition;

				if (typeDefinition == null)
				{
					// The expandable attributes can only go on types, so if this member is not a type it must be something else that only supports the self assertions
					yield return member;
					continue;
				}

				if (member.HasSelfAssertions())
					yield return member;

				// Check if the type definition only has self assertions, if so, no need to continue to trying to expand the other assertions
				if (member.CustomAttributes.Count == 1)
					continue;

				foreach (var attr in member.CustomAttributes)
				{
					if (attr.IsSelfAssertion())
						continue;


					var name = (string)attr.ConstructorArguments.First().Value;

					if (string.IsNullOrEmpty(name))
						throw new ArgumentNullException($"Value cannot be null on {attr.AttributeType} on {member}");

					IMemberDefinition matchedDefinition = typeDefinition.AllMembers().FirstOrDefault(m => m.GetFullName().EndsWith(name));

					if (matchedDefinition == null)
						throw new InvalidOperationException($"Could not find member {name} on type {typeDefinition}");

					// TODO by Mike : How to make sure what we return is now flagged for Kept/Removed?
					throw new NotImplementedException();
				}
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
				Bump();
				_realAssertions.AreEqual(expected, actual, message);
			}

			public override void Fail(string message)
			{
				_realAssertions.Fail(message);
			}
		}
	}
}
