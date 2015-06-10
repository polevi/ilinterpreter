using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace ILDisassembler
{
    public static class Globals
    {
        public static Dictionary<int, object> Cache = new Dictionary<int, object>();

        public static OpCode[] multiByteOpCodes;
        public static OpCode[] singleByteOpCodes;
        public static Module[] modules = null;

        public static void LoadOpCodes()
        {
            singleByteOpCodes = new OpCode[0x100];
            multiByteOpCodes = new OpCode[0x100];
            FieldInfo[] infoArray1 = typeof(OpCodes).GetFields();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                FieldInfo info1 = infoArray1[num1];
                if (info1.FieldType == typeof(OpCode))
                {
                    OpCode code1 = (OpCode)info1.GetValue(null);
                    ushort num2 = (ushort)code1.Value;
                    if (num2 < 0x100)
                    {
                        singleByteOpCodes[(int)num2] = code1;
                    }
                    else
                    {
                        if ((num2 & 0xff00) != 0xfe00)
                        {
                            throw new Exception("Invalid OpCode.");
                        }
                        multiByteOpCodes[num2 & 0xff] = code1;
                    }
                }
            }
        }


        public static string ProcessSpecialTypes(Type type, Assembly localAssembly)
        {
            return type.Assembly.Equals(localAssembly) ? String.Format("local.{0}", type.Name) : type.ToString();
        }

        public static string ParametersAsString(System.Reflection.ParameterInfo[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.ParameterInfo pi in parameters)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append(pi.ParameterType.FullName);
            }
            sb.Insert(0, "(");
            sb.Append(")");
            return sb.ToString();
        }
    }
}
