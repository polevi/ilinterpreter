using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ILInterpreter
{
    public class ILInstruction
    {
        private string name;
        private ushort code;
        private TValue operand;
        //private byte[] operandData;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ushort Code
        {
            get { return code; }
            set { code = value; }
        }

        public byte OpType { get; set; }

        public String ArgType { get; set; }

        public String ArgValue { get; set; }

        public TValue Operand
        {
            get { return operand; }
            set { operand = value; }
        }

        private Type operandType;
        public Type OperandT
        {
            get { return operandType; }
            set { operandType = value; }
        }

        public ILInstruction()
        {
            //
        }

        public void Prepare()
        {
            if (ArgType != null)
            {
                operandType = TypeInfo.GetType(ArgType);
                OperandType ot = (OperandType)Convert.ToInt16(OpType);
                switch (ot)
                {
                    case OperandType.InlineField:
                        throw new NotImplementedException();
                    case OperandType.InlineMethod:
                        String[] m = ArgValue.Split(new String[] { "::" }, StringSplitOptions.None);
                        operand = new TValue(MethodFromString(m[0], m[1]));
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        operand = new TValue(Convert.ToInt32(ArgValue));
                        break;
                    case OperandType.InlineType:
                        operand = new TValue(TypeInfo.GetType(ArgValue));
                        operandType = TypeInfo.GetType(ArgValue);
                        break;
                    case OperandType.InlineString:
                        operand = new TValue(ArgValue);
                        break;
                    case OperandType.ShortInlineVar:
                        operand = new TValue(Convert.ToByte(ArgValue));
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        operand = new TValue(ArgValue, OperandT); // ????
                        break;
                    case OperandType.InlineTok:
                        operand = new TValue(TypeInfo.GetType(ArgValue));
                        break;
                }
            }
        }

        /*
        public void Deserialize(String[] arr)
        {
            code = (ushort)Convert.ToInt16(arr[0], 16);
            name = arr[1];
            if (arr.Length > 2)
            {
                OperandType ot = (OperandType)Convert.ToInt16(arr[2], 16);
                switch (ot)
                {
                    case OperandType.InlineField:
                        throw new NotImplementedException();
                    case OperandType.InlineMethod:
                        String[] m = arr[3].Split(new String[] { "::" },  StringSplitOptions.None);
                        operand = MethodFromString(m[0], m[1]);
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        operand = Convert.ToInt32(arr[3], 16);
                        break;
                    case OperandType.InlineType:
                        operand = TypeInfo.GetType(arr[3]);
                        break;
                    case OperandType.InlineString:
                        operand = arr[3];
                        break;
                    case OperandType.ShortInlineVar:
                        operand = Convert.ToByte(arr[3]);
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        operand = Convert.ToInt32(arr[3]); // ????
                        break;
                    case OperandType.InlineTok:
                        operand = TypeInfo.GetType(arr[3]);
                        break;
                }
            }

        }
        */

        private object MethodFromString(String type, String name)
        {
            int pos = name.IndexOf('(');
            String n = name.Substring(0, pos);
            String a = name.Substring(pos + 1, name.Length - pos - 2);

            Type[] args = null;
            if (!String.IsNullOrEmpty(a))
            {
                String[] sargs;
                sargs = a.Split(',');
                args = new Type[sargs.Length];
                for (int i = 0; i < sargs.Length; i++)
                {
                    args[i] = TypeInfo.GetType(sargs[i]);
                }
            }
            Type t = TypeInfo.GetType(type);

            if (n.Equals(".ctor"))
                return t.GetConstructor(args != null ? args : new Type[] { });
            else
                return t.GetMethod(n, args != null ? args : new Type[] { });
        }

        public override string ToString()
        {
            return name;
        }
    }
}
