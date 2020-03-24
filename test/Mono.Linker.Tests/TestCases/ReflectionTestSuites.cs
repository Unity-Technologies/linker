using System.Collections.Generic;
using Mono.Linker.Tests.TestCasesRunner;
using NUnit.Framework;

namespace Mono.Linker.Tests.TestCases
{
	[TestFixture]
	public class ReflectionTestSuites
	{
		[TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.IsAssignableFrom))]
        public void IsAssignableFromTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetConstructor))]
        public void GetConstructorTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetEvent))]
        public void GetEventTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetField))]
        public void GetFieldTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetMethod))]
        public void GetMethodTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.CreateDelegate))]
        public void CreateDelegateTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetProperty))]
        public void GetPropertyTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetTypeConverter))]
        public void GetTypeConverterTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.ActivatorCreateInstance))]
        public void ActivatorCreateInstanceTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetNestedTypes))]
        public void GetNestedTypesTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetFields))]
        public void GetFieldsTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetEvents))]
        public void GetEventsTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetProperties))]
        public void GetPropertiesTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetMethods))]
        public void GetMethodsTests(TestCase testCase)
        {
            Run(testCase);
        }

        [TestCaseSource(typeof(TestDatabase), nameof(TestDatabase.GetConstructors))]
        public void GetConstructorsTests(TestCase testCase)
        {
            Run(testCase);
        }
        
        protected virtual void Run (TestCase testCase)
        {
	        var runner = new TestRunner (new ObjectFactory ());
	        var linkedResult = runner.Run (testCase);
	        new ResultChecker ().Check (linkedResult);
        }
        
        class TestDatabase : BaseTestDatabase
        {
            public static IEnumerable<TestCaseData> IsAssignableFrom()
            {
                return NUnitCasesBySuiteName("Reflection.IsAssignableFrom");
            }

            public static IEnumerable<TestCaseData> GetConstructor()
            {
                return NUnitCasesBySuiteName("Reflection.GetConstructor");
            }

            public static IEnumerable<TestCaseData> GetEvent()
            {
                return NUnitCasesBySuiteName("Reflection.GetEvent");
            }

            public static IEnumerable<TestCaseData> GetField()
            {
                return NUnitCasesBySuiteName("Reflection.GetField");
            }

            public static IEnumerable<TestCaseData> GetMethod()
            {
                return NUnitCasesBySuiteName("Reflection.GetMethod");
            }

            public static IEnumerable<TestCaseData> CreateDelegate()
            {
                return NUnitCasesBySuiteName("Reflection.CreateDelegate");
            }

            public static IEnumerable<TestCaseData> GetProperty()
            {
                return NUnitCasesBySuiteName("Reflection.GetProperty");
            }

            public static IEnumerable<TestCaseData> GetTypeConverter()
            {
                return NUnitCasesBySuiteName("Reflection.GetTypeConverter");
            }

            public static IEnumerable<TestCaseData> ActivatorCreateInstance()
            {
                return NUnitCasesBySuiteName("Reflection.ActivatorCreateInstance");
            }

            public static IEnumerable<TestCaseData> GetNestedTypes()
            {
                return NUnitCasesBySuiteName("Reflection.GetNestedTypes");
            }

            public static IEnumerable<TestCaseData> GetFields()
            {
                return NUnitCasesBySuiteName("Reflection.GetFields");
            }

            public static IEnumerable<TestCaseData> GetEvents()
            {
                return NUnitCasesBySuiteName("Reflection.GetEvents");
            }

            public static IEnumerable<TestCaseData> GetProperties()
            {
                return NUnitCasesBySuiteName("Reflection.GetProperties");
            }

            public static IEnumerable<TestCaseData> GetMethods()
            {
                return NUnitCasesBySuiteName("Reflection.GetMethods");
            }

            public static IEnumerable<TestCaseData> GetConstructors()
            {
                return NUnitCasesBySuiteName("Reflection.GetConstructors");
            }
        }

	}
}