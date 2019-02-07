using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.ViewModels
{
    public class TestLoginViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand ListTaskTaskCommand { get; set; }

        public TestLoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ListTaskTaskCommand = new MvxAsyncCommand(BackTask);
        }

        private async Task BackTask()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<ListTaskViewModel>();
        }
    }
}
