using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IllyaVirych.Xamarin.UI.Droid.Renderer;
using IllyaVirych.Xamarin.UI.RendererComponent;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RendererMap), typeof(MapViewRenderer))]
namespace IllyaVirych.Xamarin.UI.Droid.Renderer
{
    public class MapViewRenderer : ViewRenderer<RendererMap, TextView>
    {
        public MapViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RendererMap> e)
        {
            base.OnElementChanged(e);
            if(Control == null)
            {
                TextView textView = new TextView(Context);
                textView.Text = "Test";
                SetNativeControl(textView);
            }
        }
    }
}