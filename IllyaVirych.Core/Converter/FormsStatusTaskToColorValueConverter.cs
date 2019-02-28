using MvvmCross.Converters;
using MvvmCross.Plugin.Color;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace IllyaVirych.Core.Converter
{
    public class FormsStatusTaskToColorValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? Color.Green : Color.Yellow;
        }
    }
}
