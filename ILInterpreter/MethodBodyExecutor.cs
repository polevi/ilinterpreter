using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public class MethodBodyExecutor
    {
        public static void Execute(Package p, TStack stack, String name)
        {
            Procedure proc = p.AllMethods[name];
            ILInstruction[] arr = proc.Instructions;

            Frame frame = new Frame(p, stack);
            frame.LoadArguments(proc.GetArguments());

            int offset = 0;
            while (offset < arr.Length)
            {
                ILInstruction ili = arr[offset];
                ushort idx = (ushort)ili.Code;
                Func<Frame, ILInstruction, int> a = idx < 254 ? commands[idx] : commandsFE[idx & 0xff];
                int off = a.Invoke(frame, ili);
                switch (off)
                {
                    case -1:
                        return;
                    case 0:
                        offset++;
                        break;
                    default:
                        offset = off;
                        break;
                }
            }
        }

        //BitMobile instruction
        public static int DoCALL_LOC(Frame frame, ILInstruction ili) 
        {
            Execute(frame.Package, frame.Stack, ili.Operand.AsString);
            return 0;
        }

        public static int NotImplemented(Frame frame, ILInstruction ili)
        { 
            throw new NotImplementedException(); 
        }

        public static int DoNOP(Frame frame, ILInstruction ili)
        {
            return 0;
        }

        public static int DoBREAK(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDARG_0(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Arguments[0]);
            return 0;
        }

        public static int DoLDARG_1(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Arguments[1]);
            return 0;
        }

        public static int DoLDARG_2(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Arguments[2]);
            return 0;
        }

        public static int DoLDARG_3(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Arguments[3]);
            return 0;
        }

        public static int DoLDLOC_0(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Locals[0]);
            return 0;
        }

        public static int DoLDLOC_1(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Locals[1]);
            return 0;
        }

        public static int DoLDLOC_2(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Locals[2]);
            return 0;
        }

        public static int DoLDLOC_3(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Locals[3]);
            return 0;
        }

        public static int DoSTLOC_0(Frame frame, ILInstruction ili)
        {
            frame.Locals[0] = frame.Pop();
            return 0;
        }

        public static int DoSTLOC_1(Frame frame, ILInstruction ili)
        {
            frame.Locals[1] = frame.Pop();
            return 0;
        }

        public static int DoSTLOC_2(Frame frame, ILInstruction ili)
        {
            frame.Locals[2] = frame.Pop();
            return 0;
        }

        public static int DoSTLOC_3(Frame frame, ILInstruction ili)
        {
            frame.Locals[3] = frame.Pop();
            return 0;
        }

        public static int DoLDARG_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDARGA_S(Frame frame, ILInstruction ili)
        {
            frame.Push(TValue.CreateArgRef(ili.Operand.AsByte));
            return 0;
        }

        public static int DoSTARG_S(Frame frame, ILInstruction ili)
        {
            frame.Arguments[(byte)ili.Operand.AsByte] = frame.Pop();
            return 0;
        }

        public static int DoLDLOC_S(Frame frame, ILInstruction ili)
        {
            frame.Stack.Push(frame.Locals[ili.Operand.AsByte]);
            return 0;
        }

        public static int DoLDLOCA_S(Frame frame, ILInstruction ili)
        {
            frame.Push(TValue.CreateLocalRef(ili.Operand.AsByte));
            return 0;
        }

        public static int DoSTLOC_S(Frame frame, ILInstruction ili)
        {
            frame.Locals[ili.Operand.AsByte] = frame.Pop();
            return 0;
        }

        public static int DoIDNULL(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDC_I4_M1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDC_I4_0(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)0));
            return 0;
        }

        public static int DoLDC_I4_1(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)1));
            return 0;
        }

        public static int DoLDC_I4_2(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)2));
            return 0;
        }

        public static int DoLDC_I4_3(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)3));
            return 0;
        }

        public static int DoLDC_I4_4(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)4));
            return 0;
        }

        public static int DoLDC_I4_5(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)5));
            return 0;
        }

        public static int DoLDC_I4_6(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)6));
            return 0;
        }

        public static int DoLDC_I4_7(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)7));
            return 0;
        }

        public static int DoLDC_I4_8(Frame frame, ILInstruction ili)
        {
            frame.Push(new TValue((int)8));
            return 0;
        }

        public static int DoLDC_I4_S(Frame frame, ILInstruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        public static int DoLDC_I4(Frame frame, ILInstruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        public static int DoLDC_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDC_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDC_R8(Frame frame, ILInstruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        public static int DoDUP(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoPOP(Frame frame, ILInstruction ili)
        {
            frame.Pop();
            return 0;
        }

        public static int DoJMP(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCALL(Frame frame, ILInstruction ili)
        {
            MethodInfo mi = (MethodInfo)ili.Operand.AsObject;
            object[] args = new object[mi.GetParameters().Length];
            for (int i = args.Length - 1; i >= 0; i--)
                args[i] = frame.Pop().Box().AsObject;

            TValue result;
            if (mi.IsStatic)
                result = new TValue(mi.Invoke(null, args));
            else
            {
                TValue obj = frame.Pop();
                if (obj.IsLocalRef)
                    obj = frame.Locals[obj.Index];
                if (obj.IsArgRef)
                    obj = frame.Arguments[obj.Index];
                if (mi.IsVirtual)
                    obj = obj.ConvertTo(mi.DeclaringType);

                object instance = obj.Box().AsObject;
                result = new TValue(mi.Invoke(instance, args));
            }

            if (mi.ReturnType != typeof(void))
            {
                frame.Push(result.ConvertTo(mi.ReturnType));
            }
            return 0;
        }

        public static int DoCALLI(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoRET(Frame frame, ILInstruction ili)
        {
            return -1;
        }

        public static int DoBR_S(Frame frame, ILInstruction ili)
        {
            return ili.Operand.AsInt; 
        }

        public static int DoBR_FALSE_S(Frame frame, ILInstruction ili)
        {
            return frame.Pop().CheckIfFalseNullZero() ? ili.Operand.AsInt : 0;
        }

        public static int DoBR_TRUE_S(Frame frame, ILInstruction ili)
        {
            return frame.Pop().CheckIfFalseNullZero() ? 0: ili.Operand.AsInt;
        }

        public static int DoBEQ_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGE_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGT_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLE_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLT_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBNE_UN_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGE_UN_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGT_UN_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLE_UN_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLT_UN_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBR(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBRFALSE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBRTRUE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBEQ(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGT(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLT(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBNE_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGE_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBGT_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLE_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBLT_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSWITCH(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_U1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_U2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_I4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_U4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_R8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDIND_REF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_REF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_I4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_R8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoADD(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Add(v1,v2));
            return 0;
        }

        public static int DoSUB(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Sub(v1, v2));
            return 0;
        }

        public static int DoMUL(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Mul(v1, v2));
            return 0;
        }

        public static int DoDIV(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Div(v1, v2));
            return 0;
        }

        public static int DoDIV_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoREM(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoREM_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoAND(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoOR(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoXOR(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSHL(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSHR(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSHR_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoNEG(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoNOT(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_I4(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Pop().ConvertTo(typeof(int)));
            return 0;
        }

        public static int DoCONV_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_R8(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Pop().ConvertTo(typeof(double)));
            return 0; 
        }

        public static int DoCONV_U4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_U8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCALLVIRT(Frame frame, ILInstruction ili)
        {
            return DoCALL(frame, ili);
        }

        public static int DoCPOBJ(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDOBJ(Frame frame, ILInstruction ili)
        {
            TValue arr = frame.Pop();
            frame.Push(new TValue(((Array)arr.AsObject).GetValue(arr.Index)));
            return 0;
        }

        public static int DoLDSTR(Frame frame, ILInstruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        public static int DoNEWOBJ(Frame frame, ILInstruction ili)
        {
            ConstructorInfo mi = (ConstructorInfo)ili.Operand.AsObject;
            object[] args = new object[mi.GetParameters().Length];
            for (int i = args.Length - 1; i >= 0; i--)
                args[i] = frame.Pop().Box().AsObject;

            TValue obj = frame.Stack.Pop();

            TValue result = new TValue(mi.Invoke(args));
            
            if(obj.IsLocalRef)
                frame.Locals[obj.Index] = result.UnBox();
            if (obj.IsArgRef)
                frame.Arguments[obj.Index] = result.UnBox();

            frame.Push(result);//.ConvertTo(mi.DeclaringType));
            return 0;
        }

        public static int DoCASTCLASS(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoISINST(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_R_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoUNBOX(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoTHROW(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDFLD(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDFLDA(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTFLD(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDSFLD(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDSFLDA(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTSFLD(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTOBJ(Frame frame, ILInstruction ili)
        {
            TValue value = frame.Pop();
            TValue arr = frame.Pop();
            ((Array)arr.AsObject).SetValue(value.Box().AsObject, arr.Index);
            return 0;
        }

        public static int DoCONF_OVF_I1_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_I2_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_I4_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_I8_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_U1_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_U2_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_U4_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_U8_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_I_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONF_OVF_U_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoBOX(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Pop().Box());
            return 0;
        }

        public static int DoNEWARR(Frame frame, ILInstruction ili)
        {
            frame.Stack.Push(new TValue(ili.OperandT, frame.Stack.Pop().AsInt));
            return 0;
        }

        public static int DoLDLEN(Frame frame, ILInstruction ili)
        {
            TValue arr = frame.Stack.Pop();
            frame.Stack.Push(new TValue(((Array)arr.AsObject).Length));
            return 0;
        }

        public static int DoLDLEMA(Frame frame, ILInstruction ili)
        {
            TValue index = frame.Stack.Pop();
            TValue arr = frame.Stack.Pop();
            frame.Push(TValue.CreateArrayRef(arr.AsObject, index.AsInt));
            return 0;
        }

        public static int DoLDLEM_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_U1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_U2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_I4(Frame frame, ILInstruction ili)
        {
            TValue index = frame.Pop();
            TValue arr = frame.Pop();
            frame.Push(new TValue((int)((Array)arr.AsObject).GetValue(index.AsInt)));
            return 0;
        }

        public static int DoLDLEM_U4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_R8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDLEM_REF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_I4(Frame frame, ILInstruction ili)
        {
            TValue value = frame.Stack.Pop();
            TValue index = frame.Stack.Pop();
            TValue arr = frame.Stack.Pop();
            ((Array)arr.AsObject).SetValue(value.AsInt, index.AsInt);
            return 0;
        }

        public static int DoSTELEM_I8(Frame frame, ILInstruction ili)
        {   
            throw new NotImplementedException(); 
        }

        public static int DoSTELEM_R4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_R8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM_REF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDELEM(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTELEM(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoUNBOX_ANY(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Pop().UnBox());
            return 0;
        }

        public static int DoCONV_OVF_I1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_U1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_I2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_U2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_I4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_U4(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_I8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_U8(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoREFANYVAL(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCKFINITE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoMKREFANY(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDTOKEN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_U2(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_U1(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_OVF_U(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoADD_OVF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoADD_OVF_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoMUL_OVF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoMUL_OVF_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSUB_OVF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSUB_OVF_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoENDFINALLY(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLEAVE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLEAVE_S(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSTIND_I(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCONV_U(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoARGLIST(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCEQ(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(new TValue(v1.CompareTo(v2) == 0 ? 1 : 0));
            return 0;
        }

        public static int DoCGT(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(new TValue(v1.CompareTo(v2) > 0 ? 1 : 0));
            return 0;
        }

        public static int DoCGT_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCLT(Frame frame, ILInstruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(new TValue(v1.CompareTo(v2) < 0 ? 1 : 0));
            return 0;
        }

        public static int DoCLT_UN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLD_FTN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDVIRT_FTN(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoLDARG(Frame frame, ILInstruction ili)
        {
            frame.Push(frame.Arguments[ili.Operand.AsByte]);
            return 0;
        }

        public static int DoLDARGA(Frame frame, ILInstruction ili)
        {
            frame.Push(TValue.CreateArgRef(ili.Operand.AsByte));
            return 0;
        }

        public static int DoSTARG(Frame frame, ILInstruction ili)
        {
            frame.Arguments[(byte)ili.Operand.AsByte] = frame.Pop();
            return 0;
        }

        public static int DoLDLOC(Frame frame, ILInstruction ili)
        {
            frame.Stack.Push(frame.Locals[ili.Operand.AsByte]);
            return 0;
        }

        public static int DoLDLOCA(Frame frame, ILInstruction ili)
        {
            frame.Push(TValue.CreateLocalRef(ili.Operand.AsByte));
            return 0;
        }

        public static int DoSTLOC(Frame frame, ILInstruction ili)
        {
            frame.Locals[ili.Operand.AsByte] = frame.Pop();
            return 0;
        }

        public static int DoLOCALLOC(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoENDFILTER(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoUNALIGNED(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoVOLATILE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoTAIL(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoINITOBJ(Frame frame, ILInstruction ili)
        {
            TValue localRef = frame.Stack.Pop();
            if (!localRef.IsLocalRef)
                throw new NotImplementedException();
            frame.Locals[localRef.Index] = new TValue(ili.OperandT);
            return 0;
        }

        public static int DoCONSTRAINED(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoCPBLK(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoINITBLK(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoNO(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoRETHROW(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoSIZEOF(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoREFANYTYPE(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        public static int DoREADONLY(Frame frame, ILInstruction ili)
        { throw new NotImplementedException(); }

        private static Func<Frame,ILInstruction,int>[] commandsFE = new Func<Frame,ILInstruction,int>[] {
                DoARGLIST,
                DoCEQ,
                DoCGT,
                DoCGT_UN,
                DoCLT,
                DoCLT_UN,
                DoLD_FTN,
                DoLDVIRT_FTN,
                DoNOP,
                DoLDARG,
                DoLDARGA,
                DoSTARG,
                DoLDLOC,
                DoLDLOCA,
                DoSTLOC,
                DoLOCALLOC,
                DoNOP,
                DoENDFILTER,
                DoUNALIGNED,
                DoVOLATILE,
                DoTAIL,
                DoINITOBJ,
                DoCONSTRAINED,
                DoCPBLK,
                DoINITBLK,
                DoNO,
                DoRETHROW,
                DoNOP,
                DoSIZEOF,
                DoREFANYTYPE,
                DoREADONLY
            };

        private static Func<Frame,ILInstruction,int>[] commands = new Func<Frame,ILInstruction,int>[] {
                DoNOP,    
                DoBREAK,
                DoLDARG_0,
                DoLDARG_1,
                DoLDARG_2,
                DoLDARG_3,
                DoLDLOC_0,
                DoLDLOC_1,
                DoLDLOC_2,
                DoLDLOC_3,
                DoSTLOC_0,
                DoSTLOC_1,
                DoSTLOC_2,
                DoSTLOC_3,
                DoLDARG_S,
                DoLDARGA_S,
                DoSTARG_S,
                DoLDLOC_S,
                DoLDLOCA_S,
                DoSTLOC_S,
                DoIDNULL,
                DoLDC_I4_M1,
                DoLDC_I4_0,
                DoLDC_I4_1,
                DoLDC_I4_2,
                DoLDC_I4_3,
                DoLDC_I4_4,
                DoLDC_I4_5,
                DoLDC_I4_6,
                DoLDC_I4_7,
                DoLDC_I4_8,
                DoLDC_I4_S,
                DoLDC_I4,
                DoLDC_I8,
                DoLDC_R4,
                DoLDC_R8,
                DoNOP,
                DoDUP,
                DoPOP,
                DoJMP,
                DoCALL,
                DoCALLI,
                DoRET,
                DoBR_S,
                DoBR_FALSE_S,
                DoBR_TRUE_S,
                DoBEQ_S,
                DoBGE_S,
                DoBGT_S,
                DoBLE_S,
                DoBLT_S,
                DoBNE_UN_S,
                DoBGE_UN_S,
                DoBGT_UN_S,
                DoBLE_UN_S,
                DoBLT_UN_S,
                DoBR,
                DoBRFALSE,
                DoBRTRUE,
                DoBEQ,
                DoBGE,
                DoBGT,
                DoBLE,
                DoBLT,
                DoBNE_UN,
                DoBGE_UN,
                DoBGT_UN,
                DoBLE_UN,
                DoBLT_UN,
                DoSWITCH,
                DoLDIND_I1,
                DoLDIND_U1,
                DoLDIND_I2,
                DoLDIND_U2,
                DoLDIND_I4,
                DoLDIND_U4,
                DoLDIND_I8,
                DoLDIND_I,
                DoLDIND_R4,
                DoLDIND_R8,
                DoLDIND_REF,
                DoSTIND_REF,
                DoSTIND_I1,
                DoSTIND_I2,
                DoSTIND_I4,
                DoSTIND_I8,
                DoSTIND_R4,
                DoSTIND_R8,
                DoADD,
                DoSUB,
                DoMUL,
                DoDIV,
                DoDIV_UN,
                DoREM,
                DoREM_UN,
                DoAND,
                DoOR,
                DoXOR,
                DoSHL,
                DoSHR,
                DoSHR_UN,
                DoNEG,
                DoNOT,
                DoCONV_I1,
                DoCONV_I2,
                DoCONV_I4,
                DoCONV_I8,
                DoCONV_R4,
                DoCONV_R8,
                DoCONV_U4,
                DoCONV_U8,
                DoCALLVIRT,
                DoCPOBJ,
                DoLDOBJ,
                DoLDSTR,
                DoNEWOBJ,
                DoCASTCLASS,
                DoISINST,
                DoCONV_R_UN,
                DoNOP,
                DoNOP,
                DoUNBOX,
                DoTHROW,
                DoLDFLD,
                DoLDFLDA,
                DoSTFLD,
                DoLDSFLD,
                DoLDSFLDA,
                DoSTSFLD,
                DoSTOBJ,
                DoCONF_OVF_I1_UN,
                DoCONF_OVF_I2_UN,
                DoCONF_OVF_I4_UN,
                DoCONF_OVF_I8_UN,
                DoCONF_OVF_U1_UN,
                DoCONF_OVF_U2_UN,
                DoCONF_OVF_U4_UN,
                DoCONF_OVF_U8_UN,
                DoCONF_OVF_I_UN,
                DoCONF_OVF_U_UN,
                DoBOX,
                DoNEWARR,
                DoLDLEN,
                DoLDLEMA,
                DoLDLEM_I1,
                DoLDLEM_U1,
                DoLDLEM_I2,
                DoLDLEM_U2,
                DoLDLEM_I4,
                DoLDLEM_U4,
                DoLDLEM_I8,
                DoLDLEM_I,
                DoLDLEM_R4,
                DoLDLEM_R8,
                DoLDLEM_REF,
                DoSTELEM_I,
                DoSTELEM_I1,
                DoSTELEM_I2,
                DoSTELEM_I4,
                DoSTELEM_I8,
                DoSTELEM_R4,
                DoSTELEM_R8,
                DoSTELEM_REF,
                DoLDELEM,
                DoSTELEM,
                DoUNBOX_ANY,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoCONV_OVF_I1,
                DoCONV_OVF_U1,
                DoCONV_OVF_I2,
                DoCONV_OVF_U2,
                DoCONV_OVF_I4,
                DoCONV_OVF_U4,
                DoCONV_OVF_I8,
                DoCONV_OVF_U8,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoREFANYVAL,
                DoCKFINITE,
                DoNOP,
                DoNOP,
                DoMKREFANY,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoLDTOKEN,
                DoCONV_U2,
                DoCONV_U1,
                DoCONV_I,
                DoCONV_OVF_I,
                DoCONV_OVF_U,
                DoADD_OVF,
                DoADD_OVF_UN,
                DoMUL_OVF,
                DoMUL_OVF_UN,
                DoSUB_OVF,
                DoSUB_OVF_UN,
                DoENDFINALLY,
                DoLEAVE,
                DoLEAVE_S,
                DoSTIND_I,
                DoCONV_U,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoCALL_LOC            
            };
    }
}
