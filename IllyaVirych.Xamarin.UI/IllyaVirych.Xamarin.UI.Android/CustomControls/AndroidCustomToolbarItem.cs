using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using IllyaVirych.Xamarin.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Essentials;
using Android.Support.V7.App;
using IllyaVirych.Xamarin.UI.Droid.CustomControls;

[assembly: ExportRenderer(typeof(TaskPage), typeof(AndroidCustomToolbarItem))]
namespace IllyaVirych.Xamarin.UI.Droid.CustomControls
{
    public class AndroidCustomToolbarItem : PageRenderer
    {
        private Context _localcontext;
        private Android.Views.View _view;

        private Activity _activity;

        public AndroidCustomToolbarItem(Context context) : base(context)
        {
            _localcontext = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if(e.OldElement != null || Element == null)
            {
                return;
            }
            if (e.NewElement != null)
            {
                SetUpToolbar();
                var context = (Activity)_localcontext;
                AddView(_view);
            }
        }

        private void SetUpToolbar()
        {
            _activity = this.Context as Activity;
            _view = _activity.LayoutInflater.Inflate(Resource.Layout.customtoolbartaskview,this, false);
        }
    }
}