using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IllyaVirych.Xamarin.UI.CustomControls
{
    public class CustomMap : Map, INotifyPropertyChanged 
    {
        public static readonly BindableProperty UserLalitudeProperty = BindableProperty.Create(
                nameof(UserLalitude),
                returnType: typeof(double),
                declaringType: typeof(CustomMap),
                defaultValue: 0.0,
                defaultBindingMode: BindingMode.OneWayToSource);

        public static readonly BindableProperty UserLongitudeProperty = BindableProperty.Create(
                nameof(UserLongitude),
                returnType: typeof(double),
                declaringType: typeof(CustomMap),
                defaultValue: 0.0,
                defaultBindingMode: BindingMode.OneWayToSource);
      
        public double UserLongitude
        {
            get { return (double)GetValue(UserLongitudeProperty); }
            set { SetValue(UserLongitudeProperty, value); }
        }

        public double UserLalitude
        {
            get { return (double)GetValue(UserLalitudeProperty); }
            set { SetValue(UserLalitudeProperty, value);}
        }
    }
}
