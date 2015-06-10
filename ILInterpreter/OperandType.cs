using System;

namespace ILInterpreter
{
    // Summary:
    //     Describes the operand type of Microsoft intermediate language (MSIL) instruction.
    [Serializable]
    public enum OperandType
    {
        // Summary:
        //     The operand is a 32-bit integer branch target.
        InlineBrTarget = 0,
        //
        // Summary:
        //     The operand is a 32-bit metadata token.
        InlineField = 1,
        //
        // Summary:
        //     The operand is a 32-bit integer.
        InlineI = 2,
        //
        // Summary:
        //     The operand is a 64-bit integer.
        InlineI8 = 3,
        //
        // Summary:
        //     The operand is a 32-bit metadata token.
        InlineMethod = 4,
        //
        // Summary:
        //     No operand.
        InlineNone = 5,
        //
        // Summary:
        //     The operand is reserved and should not be used.
        [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
        InlinePhi = 6,
        //
        // Summary:
        //     The operand is a 64-bit IEEE floating point number.
        InlineR = 7,
        //
        // Summary:
        //     The operand is a 32-bit metadata signature token.
        InlineSig = 9,
        //
        // Summary:
        //     The operand is a 32-bit metadata string token.
        InlineString = 10,
        //
        // Summary:
        //     The operand is the 32-bit integer argument to a switch instruction.
        InlineSwitch = 11,
        //
        // Summary:
        //     The operand is a FieldRef, MethodRef, or TypeRef token.
        InlineTok = 12,
        //
        // Summary:
        //     The operand is a 32-bit metadata token.
        InlineType = 13,
        //
        // Summary:
        //     The operand is 16-bit integer containing the ordinal of a local variable
        //     or an argument.
        InlineVar = 14,
        //
        // Summary:
        //     The operand is an 8-bit integer branch target.
        ShortInlineBrTarget = 15,
        //
        // Summary:
        //     The operand is an 8-bit integer.
        ShortInlineI = 16,
        //
        // Summary:
        //     The operand is a 32-bit IEEE floating point number.
        ShortInlineR = 17,
        //
        // Summary:
        //     The operand is an 8-bit integer containing the ordinal of a local variable
        //     or an argumenta.
        ShortInlineVar = 18,
    }
}
