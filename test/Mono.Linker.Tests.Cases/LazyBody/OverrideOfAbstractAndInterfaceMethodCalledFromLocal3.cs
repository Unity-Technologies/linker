using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.LazyBody {
	public class OverrideOfAbstractAndInterfaceMethodCalledFromLocal3 {
		public static void Main ()
		{
			Foo b = null;
			IBar i = b;
			i.Method ();
		}

		[Kept]
		abstract class Base {
			[Kept] // FIXME : Technically this can be removed
			public abstract void Method ();
		}

		[Kept]
		[KeptBaseType (typeof (Base))]
		[KeptInterface (typeof (IBar))]
		class Foo : Base, IBar {
			[Kept]
			[ExpectedInstructionSequence(new []
			{
				"ldstr",
				"newobj",
				"throw"
			})]
			public override void Method ()
			{
				UsedByOverride ();
			}

			void UsedByOverride ()
			{
			}
		}

		[Kept]
		interface IBar {
			[Kept]
			void Method ();
		}
	}
}