using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public struct TValue : IComparable<TValue>
    {
        Type valueType;

        bool boolValue;
        sbyte sbyteValue;
        byte byteValue;
        int intValue;
        double doubleValue;
        DateTime dateTimeValue;
        TimeSpan timeSpanValue;

        object objectValue;

        byte addrFlags; //1 - local var, 2 - arguments, 3 - array
        int addrIndex;

        public TValue(bool value)
            : this()
        {
            valueType = value.GetType();
            boolValue = value;
        }

        public TValue(sbyte value)
            : this()
        {
            valueType = value.GetType();
            sbyteValue = value;
        }

        public TValue(byte value)
            : this()
        {
            valueType = value.GetType();
            byteValue = value;
        }

        public TValue(int value)
            : this()
        {
            valueType = value.GetType();
            intValue = value;
        }

        public TValue(double value)
            : this()
        {
            valueType = value.GetType();
            doubleValue = value;
        }

        public TValue(DateTime value)
            : this()
        {
            valueType = value.GetType();
            dateTimeValue = value;
        }

        public TValue(TimeSpan value)
            : this()
        {
            valueType = value.GetType();
            timeSpanValue = value;
        }

        public TValue(object value)
            : this()
        {
            if(value!=null)
                valueType = value.GetType();
            objectValue = value;
        }

        public TValue(Type t)
            : this()
        {
            valueType = t;
        }

        public TValue(Type t, int n)
            : this()
        {
            valueType = typeof(Array);
            objectValue = Array.CreateInstance(t, n);
        }

        public TValue(String value, Type t)
            : this()
        {
            valueType = t;
            TValueHelper.Parse(ref this, value);
        }

//--------------------------------------------------addresses --------------------------------------------------------------------

        public static TValue CreateLocalRef(byte n)
        {
            TValue result = new TValue();
            result.addrFlags = 1;
            result.addrIndex = n;
            return result;
        }

        public static TValue CreateArgRef(byte n)
        {
            TValue result = new TValue();
            result.addrFlags = 2;
            result.addrIndex = n;
            return result;
        }

        public static TValue CreateArrayRef(object arr, int n)
        {
            TValue result = new TValue();
            result.addrFlags = 3;
            result.addrIndex = n;
            result.objectValue = arr;
            return result;
        }

        public bool IsLocalRef
        {
            get
            {
                return addrFlags == 1;
            }
        }

        public bool IsArgRef
        {
            get
            {
                return addrFlags == 2;
            }
        }

        public bool IsArrayRef
        {
            get
            {
                return addrFlags == 3;
            }
        }

        public int Index
        {
            get
            {
                return addrIndex;
            }
        }

//--------------------------------------------------------------------------------------

        public Type ValueType
        {
            get
            {
                return valueType;
            }
        }

        public bool AsBoolean
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        public sbyte AsSByte
        {
            get { return sbyteValue; }
            set { sbyteValue = value; }
        }

        public byte AsByte
        {
            get { return byteValue; }
            set { byteValue = value; }
        }

        public int AsInt
        {
            get { return intValue; }
            set { intValue = value; }
        }

        public double AsDouble
        {
            get { return doubleValue; }
            set { doubleValue = value; }
        }

        public DateTime AsDateTime
        {
            get { return dateTimeValue; }
            set { dateTimeValue = value; }
        }

        public TimeSpan AsTimeSpan
        {
            get { return timeSpanValue; }
            set { timeSpanValue = value; }
        }

        public object AsObject
        {
            get { return objectValue; }
            set { objectValue = value; }
        }

        public String AsString
        {
            get { return objectValue == null ? null : objectValue.ToString(); }
        }

        public TValue Box()
        {
            if (valueType == null)
                return this;

            if (!valueType.IsValueType)
                return this;

            if (objectValue != null)
                return this;

            TValueHelper.Box(ref this);

            return this;
        }

        public TValue UnBox()
        {
            if (!valueType.IsValueType)
                return this;
            else
            {
                TValueHelper.Unbox(ref this);

                objectValue = null;

                return this;
            }
        }

        public TValue ConvertTo(Type t)
        {
            if (valueType != null)
            {
                if (t.Equals(valueType))
                {
                    if (objectValue == null)
                        return this;
                    else
                    {
                        return UnBox();
                    }
                }

                if (t.IsArray && valueType == typeof(Array))
                    return this;
            }

            if (t == typeof(object))
            {
                //value is already boxed
                return this;
            }

            return TValueHelper.ConvertTo(ref this, t);

            throw new NotImplementedException();
        }

        public int CompareTo(TValue other)
        {
            return this.AsInt.CompareTo(other.AsInt); //???
        }

        public bool CheckIfFalseNullZero()
        {
            if (valueType == null)
                return true;

            if (!valueType.IsValueType)
                return objectValue == null;

            return TValueHelper.CheckIfFalseNullZero(ref this);
        }

        
    }

}
