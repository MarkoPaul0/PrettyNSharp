using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPFSharpener
{
    [TypeConverter(typeof(AdvancedSize))]
    public class AdvancedSize : TypeConverter //the class is also its own type converter
    {
        private AdvancedLength _Width;
        private AdvancedLength _Height;

        public AdvancedLength Width { get { return _Width; } set { this._Width = value; } }
        public AdvancedLength Height { get { return _Height; } set { this._Height = value; } }

        public AdvancedSize()
        {

        }

        public AdvancedSize(AdvancedLength width, AdvancedLength height)
        {
            _Width = width;
            _Height = height;
        }

        #region Type Converter
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            //TODO: check if value is a string
            List<string> list = ((string)value).Split(',').ToList();
            if (list.Count != 2)
            {
                throw new ArgumentException("Expecting a string with 2 values separated by a comma!");
            }
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(AdvancedLength));
            if (converter.CanConvertFrom(typeof(string)))
            {
                return new AdvancedSize((AdvancedLength)converter.ConvertFrom(list[0]), (AdvancedLength)converter.ConvertFrom(list[1]));
            }
            return null;
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
