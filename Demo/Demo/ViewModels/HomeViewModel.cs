using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Demo.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public IMvxCommand ShowPopupCommand => new MvxAsyncCommand(ShowPopup);

        private Task ShowPopup()
        {
            return _mvxNavigationService.Navigate<SamplePopupViewModel>();
        }
        readonly IMvxNavigationService _mvxNavigationService;
        public HomeViewModel(IMvxNavigationService mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
        }

        private string _text = "Hello MvvmCross";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
