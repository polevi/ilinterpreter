using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Xml.Serialization;

namespace ILDisassembler
{
    public class ILInstruction
    {
        private OpCode code;
        private object operand;
        private byte[] operandData;
        private int offset;

        public OpCode Code
        {
            get { return code; }
            set { code = value; }
        }

        public object Operand
        {
            get { return operand; }
            set { operand = value; }
        }

        public byte[] OperandData
        {
            get { return operandData; }
            set { operandData = value; }
        }

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public ILInstruction()
        {
        }
    }
}
