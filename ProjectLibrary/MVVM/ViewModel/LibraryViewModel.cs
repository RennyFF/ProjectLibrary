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

namespace ProjectLibrary.MVVM.ViewModel
{
    class LibraryViewModel : Core.BaseViewModel
    {
        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; FavoriteGenreNames = CurrentUser.ClickedGenres != null ? new ObservableCollection<Genre>(CurrentUser.ClickedGenres.OrderBy(i => i.ClickedCountity).Take(3)) : new ObservableCollection<Genre>(); onPropertyChanged(); }
        }
        private ObservableCollection<Genre> favoriteGenreNames;
        public ObservableCollection<Genre> FavoriteGenreNames
        {
            get { return favoriteGenreNames; }
            set { favoriteGenreNames = value; onPropertyChanged(); }
        }
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
        public LibraryViewModel(INavigationService navService)
        {
            Navigation = navService;
        }
    }
}
