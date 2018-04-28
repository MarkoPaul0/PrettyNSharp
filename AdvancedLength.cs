using System;
using System.ComponentModel;

namespace PrettyNSharp
{
    [TypeConverter(typeof(AdvancedLengthConverter))]
    public class AdvancedLength
    {
        public enum UnitType
        {
            Auto = 0,
            Pixel = 1,
            Percent = 2,
            Star = 3
        };

        private UnitType _Unit = UnitType.Auto;
        public UnitType Unit { get { return _Unit; } set { _Unit = value; } }

        private double _Value = double.NaN;
        public double Value { get { return _Value; } set { _Value = value; } }

        public AdvancedLength()
        {
        }

        public AdvancedLength(double value, UnitType unit_type)
        {
            _Unit = unit_type;
            _Value = value;
        }

        public override string ToString()
        {
            if (this._Unit == UnitType.Auto)
            {
                return "Auto";
            }
            else if (this._Unit == UnitType.Star)
            {
                return "*";
            }
            else if (this._Unit == UnitType.Percent)
            {
                return this._Value + "%";
            }
            else
            {
                return this._Value.ToString();
            }
        }
    }

    
    public class AdvancedLengthConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string string_value = (string)value;
            double d_val;
            AdvancedLength.UnitType type;
            if (string_value == "Auto")
            {
                d_val = double.NaN;
                type = AdvancedLength.UnitType.Auto;
            }
            else if (string_value == "*")
            {
                d_val = double.NaN;
                type = AdvancedLength.UnitType.Star;
            }
            else
            {
                if (string_value[string_value.Length - 1] == '%')
                {
                    type = AdvancedLength.UnitType.Percent;
                    string_value = string_value.Remove(string_value.Length - 1);
                }
                else
                {
                    type = AdvancedLength.UnitType.Pixel;
                }

                try
                {
                    d_val = double.Parse(string_value);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Invalid string argument to construct Size!");
                }
            }
            return new AdvancedLength(d_val, type);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw new Exception("Unable to convert an AdvancedLength object to " + destinationType + "!");
            }
            AdvancedLength len = (AdvancedLength)value;
            if (len == null)
            {
                return null;
            }
            return this.ToString();
        }
    }
}
