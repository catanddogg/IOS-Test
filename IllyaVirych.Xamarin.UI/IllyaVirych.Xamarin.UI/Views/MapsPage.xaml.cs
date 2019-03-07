using IllyaVirych.Core.MvxInteraction;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
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
        private double _lalitude, _longitude;
        private IMvxInteraction<CoordinateAction> _interaction;
        public IMvxInteraction<CoordinateAction> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }
        public MapsPage ()
		{
			InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var set = this.CreateBindingSet<MapsPage, MapsViewModel>();
            set.Bind(this).For(v => v.Interaction).To(vm => vm.Interaction);
            set.Apply();
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<CoordinateAction> eventArgs)
        {
            _lalitude = eventArgs.Value.LalitudePin;
            _longitude = eventArgs.Value.LongitudePin;
            customMap.UserLalitude = _lalitude;
            customMap.UserLongitude = _longitude;
        }
    }
}