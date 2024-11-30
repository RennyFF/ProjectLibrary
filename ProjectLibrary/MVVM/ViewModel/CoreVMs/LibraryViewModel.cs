using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Windows;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class LibraryViewModel : BaseViewModel
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

        private ILibraryNavigationService _libraryNavigation;
        public ILibraryNavigationService LibraryNavigation
        {
            get => _libraryNavigation;
            set
            {
                _libraryNavigation = value;
                onPropertyChanged(nameof(LibraryNavigation));
            }
        }
        private double listBoxHeight;
        public double ListBoxHeight
        {
            get => listBoxHeight;
            set
            {
                listBoxHeight = value;
                UpdateVisibleFavoriteGenres();
                onPropertyChanged(nameof(ListBoxHeight));
            }
        }

        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; UpdateVisibleFavoriteGenres(); onPropertyChanged(nameof(CurrentUser)); }
        }
        private ObservableCollection<Genre> favoriteGenreNames = new ObservableCollection<Genre>();
        public ObservableCollection<Genre> FavoriteGenreNames
        {
            get { return favoriteGenreNames; }
            set { favoriteGenreNames = value; onPropertyChanged(nameof(FavoriteGenreNames)); }
        }
        #endregion
        #region Commands
        private RelayCommand logoutCommand;
        public RelayCommand LogoutCommand
        {
            get
            {
                return logoutCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        Navigation.NavigateTo<AuthViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }

        //Menu

        private RelayCommand mainNavCommand;
        public RelayCommand MainNavCommand
        {
            get
            {
                return mainNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        LibraryNavigation.NavigateLibraryTo<MainViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand catalogNavCommand;
        public RelayCommand CatalogNavCommand
        {
            get
            {
                return catalogNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        LibraryNavigation.NavigateLibraryTo<CatalogViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        #endregion
        private void UpdateVisibleFavoriteGenres()
        {
            int visibleCount = (int)(ListBoxHeight / 40);
            FavoriteGenreNames = CurrentUser.ClickedGenres != null
                ? new ObservableCollection<Genre>(CurrentUser.ClickedGenres
                    .OrderBy(i => i.ClickedCountity)
                    .Take(visibleCount))
                : new ObservableCollection<Genre>();
        }
        public LibraryViewModel(ILibraryNavigationService libraryNavService, INavigationService navservice)
        {
            LibraryNavigation = libraryNavService;
            Navigation = navservice;
            LibraryNavigation.NavigateLibraryTo<MainViewModel>();
        }
    }
}
