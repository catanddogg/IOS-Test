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
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class MainPage : MvxMasterDetailPage<MainViewModel>
	{
        private bool _firstTime = true;
		public MainPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            if (_firstTime)
            {
                ViewModel.MenuViewCommand.Execute(null);
                ViewModel.CurrentMainViewCommand.Execute(null);
               
                _firstTime = false;
            }
            base.OnAppearing();
        }
    }
}