using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Attributes.OnlyKeepUsed {
	[SetupLinkerArgument ("--used-attrs-only", "true")]
	// System.Core is being preserved in the class libraries via reflection
	// PeVerify fails on the GAC System.Core used on windows
	[SkipPeVerify("System.Core.dll")]
	class UnusedAttributeTypeOnMethodIsRemoved {
		static void Main ()
		{
			new Bar ().Method ();
		}

		[Kept]
		[KeptMember (".ctor()")]
		class Bar {
			[Foo]
			[Kept]
			public void Method ()
			{
			}
		}

		class FooAttribute : Attribute {
		}
	}
}
