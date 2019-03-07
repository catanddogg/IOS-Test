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
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuPage : MvxContentPage<MenuViewModel>
	{
		public MenuPage ()
		{
			InitializeComponent ();
            
		}
        public void ToggleClicked(object sender, EventArgs e)
        {
            if(Parent is MvxMasterDetailPage mvxMasterDetailPage)
            {
                mvxMasterDetailPage.MasterBehavior = MasterBehavior.Popover;
                mvxMasterDetailPage.IsPresented = !mvxMasterDetailPage.IsPresented;
            }
        }
	}
}