using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public class TValueHelper
    {
        static Type[] valueTypes = new Type[] {
            typeof(bool),
            typeof(sbyte),
            typeof(byte),
            typeof(int),
            typeof(double),
            typeof(DateTime),
            typeof(TimeSpan)
        };

        public delegate void A(ref TValue value);
        public delegate TValue B(ref TValue value);
        public delegate void C(ref TValue value, String fromString);
        public delegate bool D(ref TValue value);

        static D[] checkZeroActions = new D[] {
            (ref TValue x) => x.AsBoolean == false,
            (ref TValue x) => x.AsSByte == 0,
            (ref TValue x) => x.AsByte == 0,
            (ref TValue x) => x.AsInt == 0,
            (ref TValue x) => x.AsDouble == 0,
            (ref TValue x) => {throw new NotImplementedException();},
            (ref TValue x) => {throw new NotImplementedException();}
        };

        static C[] parseActions = new C[] {
            (ref TValue x, String value) => x.AsBoolean = bool.Parse(value),
            (ref TValue x, String value) => x.AsSByte = sbyte.Parse(value),
            (ref TValue x, String value) => x.AsByte = byte.Parse(value),
            (ref TValue x, String value) => x.AsInt = int.Parse(value),
            (ref TValue x, String value) => x.AsDouble = double.Parse(value),
            (ref TValue x, String value) => x.AsDateTime = DateTime.Parse(value),
            (ref TValue x, String value) => x.AsTimeSpan = TimeSpan.Parse(value)
        };

        static A[] unboxActions = new A[] {
            (ref TValue x) => x.AsBoolean = (bool)x.AsObject,
            (ref TValue x) => x.AsSByte = (sbyte)x.AsObject,
            (ref TValue x) => x.AsByte = (byte)x.AsObject,
            (ref TValue x) => x.AsInt = (int)x.AsObject,
            (ref TValue x) => x.AsDouble = (double)x.AsObject,
            (ref TValue x) => x.AsDateTime = (DateTime)x.AsObject,
            (ref TValue x) => x.AsTimeSpan = (TimeSpan)x.AsObject
        };

        static A[] boxActions = new A[] {
            (ref TValue x) => x.AsObject = x.AsBoolean,
            (ref TValue x) => x.AsObject = x.AsSByte,
            (ref TValue x) => x.AsObject = x.AsByte,
            (ref TValue x) => x.AsObject = x.AsInt,
            (ref TValue x) => x.AsObject = x.AsDouble,
            (ref TValue x) => x.AsObject = x.AsDateTime,
            (ref TValue x) => x.AsObject = x.AsTimeSpan
        };

        static B[,] convertActions = new B[,] {
            
            {//null
                (ref TValue x) => new TValue(false),
                (ref TValue x) => new TValue((sbyte)0),
                (ref TValue x) => new TValue((byte)0),
                (ref TValue x) => new TValue((int)0),
                (ref TValue x) => new TValue((double)0),
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//bool
                (ref TValue x) => x,
                (ref TValue x) => new TValue((sbyte)(x.AsBoolean?1:0)),
                (ref TValue x) => new TValue((byte)(x.AsBoolean?1:0)),
                (ref TValue x) => new TValue((int)(x.AsBoolean?1:0)),
                (ref TValue x) => new TValue((double)(x.AsBoolean?1:0)),
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//sbyte
                (ref TValue x) => new TValue(x.AsSByte!=0),
                (ref TValue x) => x,
                (ref TValue x) => new TValue((byte)(x.AsSByte)),
                (ref TValue x) => new TValue((int)(x.AsSByte)),
                (ref TValue x) => new TValue((double)(x.AsSByte)),
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//byte
                (ref TValue x) => new TValue(x.AsByte!=0),
                (ref TValue x) => new TValue((sbyte)(x.AsByte)),
                (ref TValue x) => x,
                (ref TValue x) => new TValue((int)(x.AsByte)),
                (ref TValue x) => new TValue((double)(x.AsByte)),
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//int
                (ref TValue x) => new TValue(x.AsInt!=0),
                (ref TValue x) => new TValue((sbyte)(x.AsInt)),
                (ref TValue x) => new TValue((byte)(x.AsInt)),
                (ref TValue x) => x,
                (ref TValue x) => new TValue((double)(x.AsInt)),
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//double
                (ref TValue x) => new TValue(x.AsDouble!=0),
                (ref TValue x) => new TValue((sbyte)(x.AsDouble)),
                (ref TValue x) => new TValue((byte)(x.AsDouble)),
                (ref TValue x) => new TValue((int)(x.AsDouble)),
                (ref TValue x) => x,
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//DateTime
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => x,
                (ref TValue x) => {throw new NotImplementedException();}
            },
            {//TimeSpan
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => {throw new NotImplementedException();},
                (ref TValue x) => x
            }
        };

        public static void Unbox(ref TValue value)
        {
            unboxActions[Array.IndexOf<Type>(valueTypes, value.ValueType)](ref value);
        }

        public static void Box(ref TValue value)
        {
            boxActions[Array.IndexOf<Type>(valueTypes, value.ValueType)](ref value);
        }

        public static TValue ConvertTo(ref TValue value, Type t)
        {
            return convertActions[value.ValueType == null? 0 : Array.IndexOf<Type>(valueTypes, value.ValueType) + 1, Array.IndexOf<Type>(valueTypes, t)](ref value);
        }

        public static void Parse(ref TValue value, String fromString)
        {
            parseActions[Array.IndexOf<Type>(valueTypes, value.ValueType)](ref value, fromString);
        }

        public static bool CheckIfFalseNullZero(ref TValue value)
        {
            return checkZeroActions[Array.IndexOf<Type>(valueTypes, value.ValueType)](ref value);
        }
    }
}
