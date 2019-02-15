using MvvmCross.Converters;
using System;
using System.Globalization;
using Xamarin.Essentials;

namespace IllyaVirych.IOS.Converter
{
    public class StatusValueConverter : MvxValueConverter<NetworkAccess>
    {
        protected override object Convert(NetworkAccess value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }
    }
}