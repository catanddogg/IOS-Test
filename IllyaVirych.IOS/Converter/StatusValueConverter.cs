using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Converters;
using UIKit;

namespace IllyaVirych.IOS.Converter
{
    public class StatusValueConverter : MvxValueConverter
    {
        public bool Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ?  false :  true;
        }
    }
}