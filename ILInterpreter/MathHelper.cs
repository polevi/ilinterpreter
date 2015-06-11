using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public class MathHelper
    {
        public static TValue Add(TValue a, TValue b)
        {
            Type t = a.ValueType;
            if (t == typeof(byte))
                return new TValue(a.AsByte + b.AsByte);
            if (t == typeof(int))
                return new TValue(a.AsInt + b.AsInt);
            if (t == typeof(Double))
                return new TValue(a.AsDouble + b.AsDouble);

            throw new NotImplementedException();
        }

        public static TValue Mul(TValue a, TValue b)
        {
            Type t = a.ValueType;
            if (t == typeof(byte))
                return new TValue(a.AsByte * b.AsByte);
            if (t == typeof(int))
                return new TValue(a.AsInt * b.AsInt);
            if (t == typeof(Double))
                return new TValue(a.AsDouble * b.AsDouble);

            throw new NotImplementedException();
        }

        public static TValue Div(TValue a, TValue b)
        {
            Type t = a.ValueType;
            if (t == typeof(byte))
                return new TValue(a.AsByte / b.AsByte);
            if (t == typeof(int))
                return new TValue(a.AsInt / b.AsInt);
            if (t == typeof(Double))
                return new TValue(a.AsDouble / b.AsDouble);

            throw new NotImplementedException();
        }

        public static TValue Sub(TValue a, TValue b)
        {
            Type t = a.ValueType;
            if (t == typeof(byte))
                return new TValue(a.AsByte - b.AsByte);
            if (t == typeof(int))
                return new TValue(a.AsInt - b.AsInt);
            if (t == typeof(Double))
                return new TValue(a.AsDouble - b.AsDouble);

            throw new NotImplementedException();
        }

        public static TValue Rem(TValue a, TValue b)
        {
            Type t = a.ValueType;
            if (t == typeof(byte))
                return new TValue(a.AsByte % b.AsByte);
            if (t == typeof(int))
                return new TValue(a.AsInt % b.AsInt);
            if (t == typeof(Double))
                return new TValue(a.AsDouble % b.AsDouble);

            throw new NotImplementedException();
        }
    }
}
