using IllyaVirych.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IllyaVirych.Xamarin.UI.Views
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class MapsPage : MvxContentPage<MapsViewModel>
	{
		public MapsPage ()
		{
			InitializeComponent ();
		}
	}
}