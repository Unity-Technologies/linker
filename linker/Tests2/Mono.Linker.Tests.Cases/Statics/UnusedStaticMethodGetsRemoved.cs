using System;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Statics
{
	public class UnusedStaticMethodGetsRemoved
	{
		public static void Main()
		{
			A.UsedMethod();
		}
	}

	class A
	{
		[Kept]
		public static void UsedMethod() { }

		[Removed]
		static void UnusedMethod() { }
	}
}
