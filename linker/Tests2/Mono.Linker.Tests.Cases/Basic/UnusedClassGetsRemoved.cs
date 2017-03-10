using System;
using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Basic
{
	public class UnusedClassGetsRemoved
	{
		public static void Main()
		{
		}
	}

	[Removed]
	class Unused { }
}
