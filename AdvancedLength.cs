using System;
using System.ComponentModel;

namespace WPFSharpener
{
    [TypeConverter(typeof(AdvancedLength))]
    public class AdvancedLength : TypeConverter //the class is also its own type converter
    {
        public enum UnitType
        {
            Auto = 0,
            Pixel = 1,
            Percent = 2,
            Star = 3
        };
        //TODO: rename as unit
        private UnitType _Type = UnitType.Auto;
        public UnitType Unit { get { return _Type; } set { _Type = value; } }

        private double _Value = double.NaN;
        public double Value { get { return _Value; } set { _Value = value; } }

        public AdvancedLength()
        {

        }

        public AdvancedLength(double value, UnitType unit_type)
        {
            _Type = unit_type;
            _Value = value;
        }

        #region Type Converter
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string string_value = (string)value;
            double d_val;
            UnitType type;
            if (string_value == "Auto")
            {
                d_val = double.NaN;
                type = UnitType.Auto;
            }
            else if (string_value == "*")
            {
                d_val = double.NaN;
                type = UnitType.Star;
            }
            else
            {
                if (string_value[string_value.Length - 1] == '%')
                {
                    type = UnitType.Percent;
                    string_value = string_value.Remove(string_value.Length - 1);
                }
                else
                {
                    type = UnitType.Pixel;
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

        public override string ToString()
        {
            if (this._Type == UnitType.Auto)
            {
                return "Auto";
            }
            else if (this._Type == UnitType.Star)
            {
                return "*";
            }
            else if (this._Type == UnitType.Percent)
            {
                return this._Value + "%";
            }
            else
            {
                return this._Value.ToString();
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw new Exception("Unable to convert an AdvancedSize object to " + destinationType + "!");
            }
            AdvancedLength len = (AdvancedLength)value;
            if (len == null)
            {
                return null;
            }
            return this.ToString();

        }
        #endregion
    }
}
