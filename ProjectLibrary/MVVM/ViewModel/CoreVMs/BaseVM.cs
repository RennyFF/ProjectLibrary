using ProjectLibrary.Core;
using ProjectLibrary.Utils;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class BaseVM : BaseViewModel
    {
        #region Values
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                onPropertyChanged(nameof(Navigation));
            }
        }
        #endregion
        public BaseVM(INavigationService navService)
        {
            Navigation = navService;
            Navigation.NavigateTo<AuthViewModel>();
        }
    }
}
