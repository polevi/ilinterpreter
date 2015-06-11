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
        bool boolValue;
        sbyte sbyteValue;
        byte byteValue;
        int intValue;
        double doubleValue;
        DateTime dateTimeValue;

        object objectValue;

        Type valueType;

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

        public TValue(byte value): this()
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
            if (t == typeof(bool))
            {
                boolValue = bool.Parse(value);
                return;
            }
            if (t == typeof(sbyte))
            {
                sbyteValue = sbyte.Parse(value);
                return;
            }
            if (t == typeof(byte))
            {
                byteValue = byte.Parse(value);
                return;
            }
            if (t == typeof(int))
            {
                intValue = int.Parse(value);
                return;
            }
            if (t == typeof(double))
            {
                doubleValue = double.Parse(value);
                return;
            }
            if (t == typeof(DateTime))
            {
                dateTimeValue = DateTime.Parse(value);
                return;
            }

            throw new NotImplementedException();
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
            get
            {
                return boolValue;
            }
        }

        public sbyte AsSByte
        {
            get
            {
                return sbyteValue;
            }
        }

        public byte AsByte
        {
            get
            {
                return byteValue;
            }
        }

        public int AsInt
        {
            get
            {
                return intValue;
            }
        }

        public double AsDouble
        {
            get
            {
                return doubleValue;
            }
        }

        public DateTime AsDateTime
        {
            get
            {
                return dateTimeValue;
            }
        }

        public object AsObject
        {
            get
            {
                return objectValue;
            }
        }

        public String AsString
        {
            get
            {
                return objectValue == null ? null : objectValue.ToString();
            }
        }

        public TValue Box()
        {
            if (valueType == null)
                return this;

            if (!valueType.IsValueType)
                return this;

            if (objectValue != null)
                return this;

            if (valueType == typeof(bool))
                objectValue = boolValue;
            else
                if (valueType == typeof(sbyte))
                    objectValue = sbyteValue;
                else
                    if (valueType == typeof(byte))
                        objectValue = byteValue;
                    else
                        if (valueType == typeof(int))
                            objectValue = intValue;
                        else
                            if (valueType == typeof(double))
                                objectValue = doubleValue;
                            else
                                if (valueType == typeof(DateTime))
                                    objectValue = dateTimeValue;
                                else
                                    throw new NotImplementedException();

            return this;
        }

        public TValue UnBox()
        {
            if (!valueType.IsValueType)
                return this;
            else
            {
                if (valueType == typeof(bool))
                    boolValue = (bool)objectValue;
                else
                    if (valueType == typeof(sbyte))
                        sbyteValue = (sbyte)objectValue;
                    else
                        if (valueType == typeof(byte))
                            byteValue = (byte)objectValue;
                        else
                            if (valueType == typeof(int))
                                intValue = (int)objectValue;
                            else
                                if (valueType == typeof(double))
                                    doubleValue = (double)objectValue;
                                else
                                    if (valueType == typeof(DateTime))
                                        dateTimeValue = (DateTime)objectValue;
                                    else
                                        throw new NotImplementedException();

                objectValue = null;
                return this;
            }
        }

        public TValue ConvertTo(Type t)
        {
            //TODO is there a sense in returning new object ?

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

            if (t == typeof(bool) && valueType == null)
                return new TValue(false);

            if (t == typeof(bool) && valueType == typeof(bool))
                return this;

            if (t == typeof(bool) && valueType == typeof(sbyte))
                return new TValue(sbyteValue!=0);

            if (t == typeof(bool) && valueType == typeof(byte))
                return new TValue(byteValue != 0);

            if (t == typeof(bool) && valueType == typeof(int))
                return new TValue(intValue != 0);

            if (t == typeof(bool) && valueType == typeof(double))
                return new TValue(doubleValue != 0);


            if (t == typeof(sbyte) && valueType == null)
                return new TValue((sbyte)0);

            if (t == typeof(sbyte) && valueType == typeof(bool))
                return new TValue(sbyteValue != 0);

            if (t == typeof(sbyte) && valueType == typeof(sbyte))
                return this;

            if (t == typeof(sbyte) && valueType == typeof(byte))
                return new TValue((sbyte)byteValue);

            if (t == typeof(sbyte) && valueType == typeof(int))
                return new TValue((sbyte)intValue);

            if (t == typeof(sbyte) && valueType == typeof(double))
                return new TValue((sbyte)doubleValue);


            if(t == typeof(byte) && valueType == null)
                return new TValue((byte)0);

            if (t == typeof(byte) && valueType == typeof(bool))
                return new TValue(byteValue != 0);

            if (t == typeof(byte) && valueType == typeof(sbyte))
                return new TValue((byte)sbyteValue);

            if(t == typeof(byte) && valueType == typeof(byte))
                return this;

            if(t == typeof(byte) && valueType == typeof(int))
                return new TValue((byte)intValue);

            if(t == typeof(byte) && valueType == typeof(double))
                return new TValue((byte)doubleValue);


            if(t == typeof(int) && valueType == null)
                return new TValue((int)0);

            if (t == typeof(int) && valueType == typeof(bool))
                return new TValue(intValue != 0);

            if (t == typeof(int) && valueType == typeof(sbyte))
                return new TValue((int)sbyteValue);

            if(t == typeof(int) && valueType == typeof(byte))
                return new TValue((int)byteValue);

            if(t == typeof(int) && valueType == typeof(int))
                return this;

            if(t == typeof(int) && valueType == typeof(double))
                return new TValue((int)doubleValue);


            if(t == typeof(double) && valueType == null)
                return new TValue((double)0);

            if (t == typeof(double) && valueType == typeof(bool))
                return new TValue(doubleValue != 0);

            if (t == typeof(double) && valueType == typeof(sbyte))
                return new TValue((double)sbyteValue);

            if(t == typeof(double) && valueType == typeof(byte))
                return new TValue((double)byteValue);

            if(t == typeof(double) && valueType == typeof(int))
                return new TValue((double)intValue);

            if(t == typeof(double) && valueType == typeof(double))
                return this;

            if (t == typeof(DateTime) && valueType == typeof(DateTime) && objectValue != null)
            {
                dateTimeValue = (DateTime)objectValue;
                objectValue = null;
                return this;
            }

            if (t == typeof(object))
            {
                //value is already boxed
                return this;
            }

            throw new NotImplementedException();
        }

        public int CompareTo(TValue other)
        {
            return this.AsInt.CompareTo(other.AsInt);
        }

        public bool CheckIfFalseNullZero()
        {
            if (valueType == null)
                return true;

            if (!valueType.IsValueType)
                return objectValue == null;

            if (valueType == typeof(bool))
                return boolValue == false;

            if (valueType == typeof(sbyte))
                return sbyteValue == 0;

            if (valueType == typeof(byte))
                return byteValue == 0;

            if (valueType == typeof(int))
                return intValue == 0;

            if (valueType == typeof(double))
                return doubleValue == 0;

            throw new NotImplementedException();
        }
    }

}
