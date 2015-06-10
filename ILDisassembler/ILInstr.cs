using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ILDisassembler
{
    [XmlType("ILInstruction")]
    public class ILInstr
    {
        private ILInstruction instruction;

        public ILInstr()
        {
        }

        public ILInstr(ILInstruction ili, LocalScope scope)
        {
            this.instruction = ili;
            this.Code = (ushort)instruction.Code.Value;
            this.Name = instruction.Code.ToString();
            this.OpType = (byte)instruction.Code.OperandType;
            if (instruction.Operand != null)
            {
                this.ArgType = instruction.Operand.GetType().FullName;
                this.ArgValue = GetArgValue(scope);
            }
        }

        public int GetOffset()
        {
            return instruction.Offset;
        }

        public void Prepare(Dictionary<int, int> addrs)
        {
            //patch here
            if (this.OpType == (byte)OperandType.InlineMethod)
            {
                if (this.ArgValue.StartsWith("local."))
                {
                    this.OpType = (byte)OperandType.InlineString;
                    this.Code = 0xF0;
                }
            }

            if (this.OpType == (byte)OperandType.InlineBrTarget || this.OpType == (byte)OperandType.ShortInlineBrTarget)
            {
                this.ArgValue = addrs[(int)instruction.Operand].ToString();
            }

        }

        public ushort Code {get;set;}

        public String Name {get;set;}

        public byte OpType {get;set;}

        public String ArgType {get;set;}

        public String ArgValue {get;set;}

        private String GetArgValue(LocalScope scope)
        {
            if (instruction.Operand == null)
                return "";
            else
            {
                bool isLocalCall;

                string s = "";
                if (instruction.Operand != null)
                {
                    switch (instruction.Code.OperandType)
                    {
                        case OperandType.InlineField:
                            System.Reflection.FieldInfo fOperand = ((System.Reflection.FieldInfo)instruction.Operand);
                            s = Globals.ProcessSpecialTypes(fOperand.ReflectedType, scope.LocalAssembly) + "::" + fOperand.Name + "";
                            break;
                        case OperandType.InlineMethod:
                            try
                            {
                                System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)instruction.Operand;
                                String t = Globals.ProcessSpecialTypes(mOperand.ReflectedType, scope.LocalAssembly);
                                s = t + "::" + mOperand.Name + Globals.ParametersAsString(mOperand.GetParameters());
                                isLocalCall = t.StartsWith("local");
                            }
                            catch
                            {
                                try
                                {
                                    System.Reflection.ConstructorInfo mOperand = (System.Reflection.ConstructorInfo)instruction.Operand;
                                    s = Globals.ProcessSpecialTypes(mOperand.ReflectedType, scope.LocalAssembly) +
                                        "::" + mOperand.Name + Globals.ParametersAsString(mOperand.GetParameters());
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case OperandType.ShortInlineBrTarget:
                        case OperandType.InlineBrTarget:
                            s = instruction.Offset.ToString();
                            break;
                        case OperandType.InlineType:
                            s = Globals.ProcessSpecialTypes((Type)instruction.Operand, scope.LocalAssembly);
                            break;
                        case OperandType.InlineString:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.ShortInlineVar:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.InlineI:
                        case OperandType.InlineI8:
                        case OperandType.InlineR:
                        case OperandType.ShortInlineI:
                        case OperandType.ShortInlineR:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.InlineTok:
                            if (instruction.Operand is Type)
                                s = ((Type)instruction.Operand).FullName;
                            else
                                s = "not supported";
                            break;

                        default: s = "not supported"; break;
                    }
                }

                return s;
            }

        }

    }
}
