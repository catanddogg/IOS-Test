using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Plugin.Color;
using MvvmCross.UI;
using UIKit;

namespace IllyaVirych.IOS.Converter
{
    
    //public MvxColor ColorC
    //{
    //    get
    //    {
    //        return 
    //    }
    //}
    public class ColorValueConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? new MvxColor(0, 128, 0, 255) : new MvxColor(255, 255, 0, 255);
        }
    }
}