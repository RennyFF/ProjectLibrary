using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.View;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    public interface ILibraryNavigationService
    {
        Core.BaseViewModel CurrentLibraryView { get; }
        void NavigateLibraryTo<T>(object Param = null) where T : Core.BaseViewModel;
    }
    public class LibraryNavigationService : ObservableObject, ILibraryNavigationService
    {
        private Core.BaseViewModel _currentLibraryView;
        private readonly Func<Type, Core.BaseViewModel> _viewModelFactory;

        public Core.BaseViewModel CurrentLibraryView
        {
            get { return _currentLibraryView; }
            private set { _currentLibraryView = value; onPropertyChanged(); }
        }


        public LibraryNavigationService(Func<Type, Core.BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateLibraryTo<TViewModel>(object Param = null) where TViewModel : Core.BaseViewModel
        {
            Core.BaseViewModel ViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (ViewModel is PreviewBookViewModel previewBookViewModel && Param is int BookId)
            {
                previewBookViewModel.GetPreviewedBook(BookId);
            }
            CurrentLibraryView = ViewModel;
        }
    }
}
