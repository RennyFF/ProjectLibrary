using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.ViewModel
{
    class LibraryViewModel : Core.BaseViewModel
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
        public RelayCommand NavigateToLib { get; set; }
        public LibraryViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToLib = new RelayCommand(o => { Navigation.NavigateTo<AuthViewModel>(); }, o => true);
        }
    }
}
