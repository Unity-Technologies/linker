using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using Mono.Linker.Tests.Cases.TypeForwarding.Dependencies;

namespace Mono.Linker.Tests.Cases.TypeForwarding
{
	// Actions:
	// link - This assembly
	// copyused - Forwarder.dll and Implementation.dll
	[SetupLinkerAction ("link", "test")]
	[SetupLinkerUserAction ("copyused")]
	[KeepTypeForwarderOnlyAssemblies ("false")]

	[SetupCompileBefore ("Forwarder.dll", new[] { "Dependencies/ReferenceImplementationLibrary.cs" }, defines: new[] { "INCLUDE_REFERENCE_IMPL" })]

	// After compiling the test case we then replace the reference impl with implementation + type forwarder
	[SetupCompileAfter ("Implementation.dll", new[] { "Dependencies/ImplementationLibrary.cs" })]
	[SetupCompileAfter ("Forwarder.dll", new[] { "Dependencies/ForwarderLibrary.cs" }, references: new[] { "Implementation.dll" })]

	[RemovedAssembly ("Forwarder.dll")]
	[KeptMemberInAssembly ("Implementation.dll", typeof (ImplementationLibrary), "GetSomeValue()")]
	class UsedForwarderWithAssemblyCopyUsed
	{
		static void Main ()
		{
			new ImplementationLibrary ().GetSomeValue ();
		}
	}
}
