using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using System.Windows;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
using ProjectLibrary.Core.Types;
using ProjectLibrary.Core.Types.Client;

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
        private string searchString;
        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; onPropertyChanged(nameof(SearchString)); }
        }

        private UserShort currentUser;
        public UserShort CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; onPropertyChanged(nameof(CurrentUser)); FavoriteGenreNames = value.ClickedGenres != null
                ? new ObservableCollection<FavGenreType>(value.ClickedGenres
                    .OrderBy(i => i.ClickedCountity).Take(3)
                    ) : new ObservableCollection<FavGenreType>();
            }
        }
        private ObservableCollection<FavGenreType> favoriteGenreNames = new ObservableCollection<FavGenreType>();
        public ObservableCollection<FavGenreType> FavoriteGenreNames
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
                        SearchString = string.Empty;
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
                        SearchString = string.Empty;
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
        private RelayCommand historyNavCommand;
        public RelayCommand HistoryNavCommand
        {
            get
            {
                return historyNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        SearchString = string.Empty;
                        LibraryNavigation.NavigateLibraryTo<HistoryViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand favoriteNavCommand;
        public RelayCommand FavoriteNavCommand
        {
            get
            {
                return favoriteNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        SearchString = string.Empty;
                        LibraryNavigation.NavigateLibraryTo<FavoriteBooksViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand authorsNavCommand;
        public RelayCommand AuthorsNavCommand
        {
            get
            {
                return authorsNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        SearchString = string.Empty;
                        LibraryNavigation.NavigateLibraryTo<AuthorsViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand genresNavCommand;
        public RelayCommand GenresNavCommand
        {
            get
            {
                return genresNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        SearchString = string.Empty;
                        LibraryNavigation.NavigateLibraryTo<GenresViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand searchNavCommand;
        public RelayCommand SearchNavCommand
        {
            get
            {
                return searchNavCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        LibraryNavigation.NavigateLibraryTo<SearchViewModel>(SearchString);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        private RelayCommand goToPreviewGenre;
        public RelayCommand GoToPreviewGenre
        {
            get
            {
                return goToPreviewGenre ??= new RelayCommand(obj =>
                {
                    if (obj is FavGenreType SelectedFavoriteGenre)
                    {
                        SearchString = string.Empty;
                        Constants.PreviousVM = new List<PreviousViewModels?>();
                        Constants.PreviousVM.Add(PreviousViewModels.MainVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewGenreViewModel>(SelectedFavoriteGenre.Id);
                        return;
                    }
                }, obj => true);
            }
        }
        #endregion
        public LibraryViewModel(ILibraryNavigationService libraryNavService, INavigationService navservice)
        {
            LibraryNavigation = libraryNavService;
            Navigation = navservice;
            LibraryNavigation.NavigateLibraryTo<MainViewModel>();
        }
    }
}
