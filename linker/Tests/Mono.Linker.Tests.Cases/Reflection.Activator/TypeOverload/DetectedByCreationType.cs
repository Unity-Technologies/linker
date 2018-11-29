using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.Activator.TypeOverload {
	public class DetectedByCreationType {
		public static void Main ()
		{
			var tmp = System.Activator.CreateInstance (typeof (Foo));
			HereToUseCreatedInstance (tmp);
		}

		[Kept]
		static void HereToUseCreatedInstance (object arg)
		{
		}

		[Kept]
		class Foo {
			[Kept]
			public Foo ()
			{
			}

			public Foo (int arg)
			{
			}
		}
	}
}