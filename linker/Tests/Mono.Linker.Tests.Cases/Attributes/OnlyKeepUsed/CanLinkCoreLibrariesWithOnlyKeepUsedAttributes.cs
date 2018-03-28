using System;
using System.Timers;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Attributes.OnlyKeepUsed {
	[SetupLinkerCoreAction ("link")]
	[SetupLinkerArgument ("--used-attrs-only", "true")]
	[Reference ("System.dll")]
	// System.Core is being preserved in the class libraries via reflection
	// PeVerify fails on the GAC System.Core used on windows
	[SkipPeVerify("System.Core.dll")]
	// Fails with `Runtime critical type System.Reflection.CustomAttributeData not found`
	[SkipPeVerify (SkipPeVerifyForToolchian.Pedump)]
	class CanLinkCoreLibrariesWithOnlyKeepUsedAttributes {
		static void Main ()
		{
			// Use something from System so that the entire reference isn't linked away
			var system = new Timer ();
		}
	}
}
