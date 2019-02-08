using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.LazyBody {
	public class SimpleSetter {
		public static void Main()
		{
			UsedToMarkMethod (null);
		}

		[Kept]
		static void UsedToMarkMethod (Foo f)
		{
			f.Property = string.Empty;
		}

		[Kept]
		class Foo {
			[Kept]
			public string Property { get; [Kept] [ExpectBodyModified] set; }
		}
	}
}