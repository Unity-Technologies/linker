using System;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Basic
{
	public class UsedVirtualMethodNotRemoved
	{
		public static void Main()
		{
			new B(); new Base().Call();
		}

		class Base
		{
			[Kept]
			public virtual void Call() { }
		}

		class B : Base
		{
			[Kept]
			public override void Call() { }
		}
	}
}
