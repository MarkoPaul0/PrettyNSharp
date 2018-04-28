using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPFSharpener
{
    [TypeConverter(typeof(AdvancedSizeConverter))]
    public class AdvancedSize
    {
        private AdvancedLength _Width;
        private AdvancedLength _Height;

        public AdvancedLength Width { get { return _Width; } set { this._Width = value; } }
        public AdvancedLength Height { get { return _Height; } set { this._Height = value; } }

        public AdvancedSize()
        {
            this._Height = new AdvancedLength();
            this._Width = new AdvancedLength();
        }

        public AdvancedSize(AdvancedLength width, AdvancedLength height)
        {
            _Width = width;
            _Height = height;
        }

        #region Type Converter

        #endregion
    };

    
    public class AdvancedSizeConverter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(AdvancedLength));
            List<string> list = ((string)value).Split(',').ToList();
            if (list.Count == 1) // e.g. VectorSize="Auto" is interpreted as VectorSize="Auto,Auto"
            {
                return new AdvancedSize((AdvancedLength)converter.ConvertFrom(list[0]), (AdvancedLength)converter.ConvertFrom(list[0]));
            }
            else if (list.Count == 2) // e.g. VectorSize="200,Auto"
            {
                return new AdvancedSize((AdvancedLength)converter.ConvertFrom(list[0]), (AdvancedLength)converter.ConvertFrom(list[1]));
            }
            else
            {
                throw new ArgumentException("Expecting a string with 2 values separated by a comma!");
            }
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
    }
}
