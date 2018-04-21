using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSharpener
{
    [TypeConverter(typeof(AdvancedSize))]
    public class AdvancedSize : TypeConverter
    {
        public enum SizeType
        {
            Auto = 0,
            Pixel = 1,
            Percent = 2
        };
        
        //TODO: make each dimension an AdvancedLength class
        private SizeType _WidthType = SizeType.Auto;
        private SizeType _HeightType = SizeType.Auto;
        private double _Width = double.NaN;
        private double _Height = double.NaN;


        public SizeType WidthType { get { return _WidthType; } }
        public SizeType HeightType { get { return _HeightType; } }
        public double Width { get { return _Width; } set { this._Width = value; } }
        public double Height { get { return _Height; } set { this._Height = value; } }

        public AdvancedSize()
        {

        }

        public AdvancedSize(string str)
        {
            List<string> list = str.Split(',').ToList();

            if (list.Count != 2)
            {
                throw new ArgumentException("Expecting a string with 2 values separated by a comma!");
            }

            try
            {
                _Width = double.Parse(list[0]);
                _WidthType = SizeType.Pixel;
            }
            catch (Exception ex)
            {
                if (list[0] == "Auto")
                {
                    _Width = double.NaN;
                    _WidthType = SizeType.Auto;
                }
                else if (list[0].Last() == '%')
                {
                    _Width = double.Parse(list[0].Remove(list[0].Count() - 1));
                    _WidthType = SizeType.Percent;
                }
                else
                {
                    throw new ArgumentException("Invalid string argument to construct Size!");
                }
            }
            try
            {
                _Height = double.Parse(list[1]);
                _HeightType = SizeType.Pixel;
            }
            catch (Exception ex)
            {
                if (list[1] == "Auto")
                {
                    _Height = double.NaN;
                    _HeightType = SizeType.Auto;
                }
                else if (list[1].Last() == '%')
                {
                    _Height = double.Parse(list[1].Remove(list[1].Count() - 1));
                    _HeightType = SizeType.Percent;
                }
                else
                {
                    throw new ArgumentException("Invalid string argument to construct Size!");
                }
            }
        }

        #region Type Converter
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return new AdvancedSize((string)value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            AdvancedSize size = (AdvancedSize)value;
            return value == null ? null : size.Width + "," + size.Height;
        }
        #endregion
    };
}
