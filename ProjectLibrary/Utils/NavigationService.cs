﻿using ProjectLibrary.Core;
using ProjectLibrary.MVVM.View;
using ProjectLibrary.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    public interface INavigationService
    {
        Core.BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : Core.BaseViewModel;
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
            Core.BaseViewModel base_viewModel = _viewModelFactory.Invoke(typeof(AuthViewModel)); // base View
            CurrentView = base_viewModel;
        }

        public void NavigateTo<TViewModel>() where TViewModel : Core.BaseViewModel
        {
            Core.BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
