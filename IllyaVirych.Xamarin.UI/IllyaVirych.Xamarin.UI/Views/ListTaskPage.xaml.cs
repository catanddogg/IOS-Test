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
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true, Animated = false)]
    public partial class ListTaskPage : MvxContentPage<ListTaskViewModel>
	{
        public static BindableProperty bindableProperty;

        public int _heightRequest;

        public ListTaskPage ()
		{
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.ShowMenuViewModelCommand.Execute();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            _heightRequest = (int)(width - 20) / 2;
            FlowListViewName.RowHeight = _heightRequest;
            base.OnSizeAllocated(width, height);
        }
    }
}