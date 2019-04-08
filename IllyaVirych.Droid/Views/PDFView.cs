using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.Droid.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.PdfRasterizer;

namespace IllyaVirych.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class PDFView : BaseFragment<PDFViewModel>
    {
        #region Constructors       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            Test();
            return view;
        }
        #endregion

        private async Task  Test()
        {
            try
            {
                const string documentUrl = "https://developer.xamarin.com/guides/xamarin-forms/getting-started/introduction-to-xamarin-forms/offline.pdf";
                const bool forceRasterize = false;

                var rasterizer = CrossPdfRasterizer.Current;

                var document = await rasterizer.RasterizeAsync(documentUrl, forceRasterize);
                var pageImages = document.Pages.Select((p) => p.Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to rasterize provided document: " + ex);
            }
        }

        #region Override
        protected override int FragmentId => Resource.Layout.pdfview;
        #endregion
    }
}