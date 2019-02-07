using System.Globalization;
using MvvmCross.Plugin.Color;
using MvvmCross.UI;

namespace IllyaVirych.Droid.Converter
{
    public class ColorValueConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool value, object parameter, CultureInfo culture)
        {            
            return value ? new MvxColor(0, 128, 0, 255) : new MvxColor(255, 255, 0, 255);
        }
    }
}