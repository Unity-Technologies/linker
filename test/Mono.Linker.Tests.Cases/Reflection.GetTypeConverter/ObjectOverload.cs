using System;
using System.ComponentModel;
using System.Globalization;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

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
    public class ObjectOverload
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
            var number = double.Parse("-2147483648", NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForString()
        {
            var number = "blah";
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForInt()
        {
            var number = int.Parse("-2147483648", NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForLong()
        {
            var number = long.Parse("-2147483648", NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForFloat()
        {
            var number = float.Parse("-2147483648", NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForDecimal()
        {
            var number = Decimal.Parse("-2147483648", NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }

        [Kept]
        static void MethodForObject()
        {
            var number = new object();
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(number.GetType());
            converter.ConvertTo(number, typeof(Int32));
        }
    }
}
