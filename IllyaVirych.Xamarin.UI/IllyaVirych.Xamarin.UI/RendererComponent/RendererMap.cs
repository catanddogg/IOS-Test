using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IllyaVirych.Xamarin.UI.RendererComponent
{
    public class RendererMap : Map, INotifyPropertyChanged 
    {
        public List<RendererPin> RendererPins { get; set; }

        //public static readonly BindableProperty UserLonglitudeProperty = BindableProperty.Create(
        //   nameof(UserLongitude),
        //   returnType: typeof(double),
        //   declaringType: typeof(RendererMap),
        //   defaultValue: 0.0,
        //   defaultBindingMode: BindingMode.OneWayToSource );
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly BindableProperty UserLalitudeProperty = BindableProperty.Create(
                nameof(UserLalitude),
                returnType: typeof(double),
                declaringType: typeof(RendererMap),
                defaultValue: 0.0,
                defaultBindingMode: BindingMode.OneWayToSource);

        public static readonly BindableProperty UserLongitudeProperty = BindableProperty.Create(
                nameof(UserLongitude),
                returnType: typeof(double),
                declaringType: typeof(RendererMap),
                defaultValue: 0.0,
                defaultBindingMode: BindingMode.OneWayToSource);
      
        public double UserLongitude
        {
            get { return (double)GetValue(UserLongitudeProperty); }
            set { SetValue(UserLongitudeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserLongitude"));
            }
        }

        public double UserLalitude
        {
            get { return (double)GetValue(UserLalitudeProperty); }
            set { SetValue(UserLalitudeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserLalitude"));
            }
        }

        //public double UserLongitude
        //{
        //    get { return (double)GetValue(UserLonglitudeProperty); }
        //    set { SetValue(UserLonglitudeProperty, value); }
        //}
    }
}
