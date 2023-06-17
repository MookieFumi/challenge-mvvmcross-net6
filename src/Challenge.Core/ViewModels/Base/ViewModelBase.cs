using Acr.UserDialogs;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Challenge.Core.ViewModels.Base
{
    public class ViewModelBase : MvxViewModel
    {
        protected readonly IMvxNavigationService NavigationService;
        private readonly IUserDialogs _dialogsService;

        public ViewModelBase(IMvxNavigationService navigationService, IUserDialogs dialogsService)
        {
            NavigationService = navigationService;
            _dialogsService = dialogsService;
        }

        protected void ShowLoading()
        {
            _dialogsService.Loading("Loading data")?.Show();
        }

        protected void HideLoading()
        {
            _dialogsService.Loading()?.Hide();
        }
    }

    public class ViewModelBase<T> : ViewModelBase, IMvxViewModel<T>
    {
        public ViewModelBase(IMvxNavigationService navigationService, IUserDialogs dialogsService) : base(navigationService, dialogsService)
        {
        }

        public virtual void Prepare(T parameter)
        {
        }
    }
}

