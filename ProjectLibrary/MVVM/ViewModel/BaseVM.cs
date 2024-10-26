using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.ViewModel
{
    class BaseVM : Core.BaseViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                onPropertyChanged();
            }
        }
        public RelayCommand NavigateToHome { get; set; }
        public BaseVM(INavigationService navService)
        {
            Navigation = navService;
            Navigation.NavigateTo<AuthViewModel>();
        }
    }
}
