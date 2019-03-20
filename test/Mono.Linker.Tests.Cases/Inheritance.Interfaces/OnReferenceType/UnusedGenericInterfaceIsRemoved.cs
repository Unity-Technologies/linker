using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Inheritance.Interfaces.OnReferenceType {
	public class UnusedGenericInterfaceIsRemoved {
		public static void Main ()
		{
			var fb = new Foo ();
			IFoo<object> fo = fb;
			fo.Method (null);
			
			IFoo<int> fi = fb;
			fi.Method (0);
		}

		[Kept]
		interface IFoo<T> {
			[Kept]
			void Method (T arg);
		}

		[Kept]
		[KeptMember (".ctor()")]
		[KeptInterface (typeof (IFoo<object>))]
		[KeptInterface (typeof (IFoo<int>))]
		class Foo : IFoo<object>, IFoo<int>, IFoo<string>, IFoo<Bar> {
			[Kept]
			public void Method (object arg)
			{
			}

			[Kept]
			public void Method (int arg)
			{
			}

			public void Method (string arg)
			{
			}

			public void Method (Bar arg)
			{
			}
		}

		class Bar {
		}
	}
}