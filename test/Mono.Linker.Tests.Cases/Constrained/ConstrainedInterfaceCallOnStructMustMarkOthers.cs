using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Constrained {
	[Define ("IL_INCLUDED")]
	[SetupCompileBefore ("library.dll", new []{"Dependencies/AssemblyWithConstrainedCall.il"})]

	[KeptMemberInAssembly ("library.dll", "Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One", "Method()")]

	// Must be kept because it's an interface method
	[KeptMemberInAssembly ("library.dll", "Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/Two", "Method()")]
	public class ConstrainedInterfaceCallOnStructMustMarkOthers {
		public static void Main()
		{
#if IL_INCLUDED
			Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall.UseTheCode ();
#endif
		}
	}
}