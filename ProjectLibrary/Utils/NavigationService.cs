using ProjectLibrary.Core;
using ProjectLibrary.MVVM.View;
using ProjectLibrary.MVVM.ViewModel;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    public interface INavigationService
    {
        Core.BaseViewModel CurrentView { get; }
        void NavigateTo<T>(object Param = null) where T : Core.BaseViewModel;
    }
    public class NavigationService : ObservableObject, INavigationService
    {
        private Core.BaseViewModel _currentView;
        private readonly Func<Type, Core.BaseViewModel> _viewModelFactory;

        public Core.BaseViewModel CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; onPropertyChanged(); }
        }


        public NavigationService(Func<Type, Core.BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>(object Param = null) where TViewModel : Core.BaseViewModel
        {
            Core.BaseViewModel ViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (ViewModel is LibraryViewModel LibraryViewModel && Param is User UserDataContext)
            {
                LibraryViewModel.CurrentUser = UserDataContext;
            }
            CurrentView = ViewModel;
        }
    }
}
