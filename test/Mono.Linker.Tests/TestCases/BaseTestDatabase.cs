using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using System.Runtime.CompilerServices;
using System.IO;
using Mono.Linker.Tests.TestCasesRunner;

namespace Mono.Linker.Tests.TestCases
{
	public class BaseTestDatabase
	{
		private static TestCase[] _cachedAllCases;
		
		public static TestCaseCollector CreateCollector ()
		{
			GetDirectoryPaths (out string rootSourceDirectory, out string testCaseAssemblyPath);
			return new TestCaseCollector (rootSourceDirectory, testCaseAssemblyPath);
		}

		static IEnumerable<TestCase> AllCases ()
		{
			if (_cachedAllCases == null)
				_cachedAllCases = CreateCollector ()
					.Collect ()
					.OrderBy (c => c.DisplayName)
					.ToArray ();

			return _cachedAllCases;
		}

		protected static IEnumerable<TestCaseData> NUnitCasesBySuiteName(string suiteName)
		{
			return AllCases()
				.Where(c => c.TestSuiteDirectory.FileName == suiteName)
				.Select(c => CreateNUnitTestCase(c, c.DisplayName.Substring(suiteName.Length + 1)))
				.OrderBy(c => c.TestName);
		}

		static TestCaseData CreateNUnitTestCase(TestCase testCase, string displayName)
		{
			var data = new TestCaseData(testCase);
			data.SetName(displayName);
			return data;
		}

		static void GetDirectoryPaths(out string rootSourceDirectory, out string testCaseAssemblyPath, [CallerFilePath] string thisFile = null)
		{

#if DEBUG
			var configDirectoryName = "Debug";
#else
			var configDirectoryName = "Release";
#endif

#if NETCOREAPP3_0
			var tfm = "netcoreapp3.0";
#elif NET471
			var tfm = "net471";
#else
			var tfm = "";
#endif

#if ILLINK
			// Deterministic builds sanitize source paths, so CallerFilePathAttribute gives an incorrect path.
			// Instead, get the testcase dll based on the working directory of the test runner.

			// working directory is artifacts/bin/Mono.Linker.Tests/<config>/<tfm>
			var artifactsBinDir = Path.Combine (Directory.GetCurrentDirectory (), "..", "..", "..");
			rootSourceDirectory = Path.GetFullPath (Path.Combine (artifactsBinDir, "..", "..", "test", "Mono.Linker.Tests.Cases"));
			testCaseAssemblyPath = Path.GetFullPath (Path.Combine (artifactsBinDir, "Mono.Linker.Tests.Cases", configDirectoryName, tfm, "Mono.Linker.Tests.Cases.dll"));
#else
			var thisDirectory = Path.GetDirectoryName (thisFile);
			rootSourceDirectory = Path.GetFullPath (Path.Combine (thisDirectory, "..", "..", "Mono.Linker.Tests.Cases"));
			testCaseAssemblyPath = Path.GetFullPath (Path.Combine (rootSourceDirectory, "bin", configDirectoryName, tfm, "Mono.Linker.Tests.Cases.dll"));
#endif // ILLINK
		}
	}
}
