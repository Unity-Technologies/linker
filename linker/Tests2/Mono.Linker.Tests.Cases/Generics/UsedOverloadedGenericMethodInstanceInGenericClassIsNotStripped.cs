﻿using Mono.Linker.Tests.Cases.Expectations;

namespace Mono.Linker.Tests.Cases.Generics
{
	public class UsedOverloadedGenericMethodInstanceInGenericClassIsNotStripped
	{
		public static void Main()
		{
			B<int>.Method(1);
		}

		class B<TBase>
		{
			[Removed]
			public static void Method<T>(T value) { }

			[Kept]
			public static void Method(TBase value) { }
		}
	}
}