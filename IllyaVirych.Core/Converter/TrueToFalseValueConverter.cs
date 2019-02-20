using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IllyaVirych.Core.Converter
{
    public class TrueToFalseValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == false)
            {
                return true;
            }
            return false;
        }
    }
}
