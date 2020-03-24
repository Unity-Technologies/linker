using System.ComponentModel;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.GetTypeConverter
{
    [Reference("System.dll")]
    [SetupLinkerCoreAction("link")]
    [KeptMemberInAssembly("System.dll", typeof(DoubleConverter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(StringConverter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(Int32Converter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(Int64Converter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(SingleConverter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(DecimalConverter), ".ctor()")]
    [KeptMemberInAssembly("System.dll", typeof(TypeConverter), ".ctor()")]
    public class TypeOverload
    {
        public static void Main()
        {
            MethodForDouble();
            MethodForString();
            MethodForInt();
            MethodForLong();
            MethodForFloat();
            MethodForDecimal();
            MethodForObject();
        }

        [Kept]
        static void MethodForDouble()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(double));
        }

        [Kept]
        static void MethodForString()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(string));
        }

        [Kept]
        static void MethodForInt()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(int));
        }

        [Kept]
        static void MethodForLong()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(long));
        }

        [Kept]
        static void MethodForFloat()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(float));
        }

        [Kept]
        static void MethodForDecimal()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(decimal));
        }

        [Kept]
        static void MethodForObject()
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(object));
        }
    }
}
