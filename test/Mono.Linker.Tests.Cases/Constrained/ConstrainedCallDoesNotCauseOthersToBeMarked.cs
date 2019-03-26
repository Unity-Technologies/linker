using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Constrained {
	[Define ("IL_INCLUDED")]
	[SetupCompileBefore ("library.dll", new []{"Dependencies/AssemblyWithConstrainedCallOnClass.il"})]
	
	[KeptMemberInAssembly ("library.dll", "Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One", "Method()")]
	
	// Can be removed since it's an override of a virtual and all known calls to `Base.Method()` were constrained to `One`
	[RemovedMemberInAssembly ("library.dll", "Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Two", "Method()")]
	public class ConstrainedCallDoesNotCauseOthersToBeMarked {
		public static void Main()
		{
#if IL_INCLUDED
			Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass.UseTheCode ();
#endif
		}
	}
}