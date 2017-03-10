using System;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Basic
{
	class UnusedVirtualMethodRemoved
	{
		public static void Main()
		{
			new Base().Call();
		}

		class Base
		{
			[Kept]
			public virtual void Call() { }
		}

		class B : Base
		{
			[Removed]
			public override void Call() { }
		}
	}
}
