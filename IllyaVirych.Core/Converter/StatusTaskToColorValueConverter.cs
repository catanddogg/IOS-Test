using MvvmCross.Plugin.Color;
using MvvmCross.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IllyaVirych.Core.Converter
{
    public class StatusTaskToColorValueConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? new MvxColor(0, 128, 0, 255) : new MvxColor(255, 255, 0, 255);
        }
    }
}
