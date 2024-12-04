using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class FavoriteBooksViewModel : Core.BaseViewModel
    {
        #region Values
        private int CountityOnPage = 27;
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
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
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; onPropertyChanged(nameof(IsLoading)); }
        }
        private bool isHasItems;
        public bool IsHasItems
        {
            get { return isHasItems; }
            set { isHasItems = value; onPropertyChanged(nameof(IsHasItems)); }
        }

        private ObservableCollection<BookCard> favoriteBooks = new();
        public ObservableCollection<BookCard> FavoriteBooks
        {
            get { return favoriteBooks; }
            set { favoriteBooks = value; }
        }

        private int allPages;
        public int AllPages
        {
            get { return allPages; }
            set { allPages = value; onPropertyChanged(nameof(AllPages)); }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; onPropertyChanged(nameof(CurrentPage)); }
        }
        #endregion
        #region Commands

        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    if (obj is BookCard SelectedBook)
                    {
                        Constants.PreviousVM = PreviousViewModels.FavoriteVM;
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                    }
                }, obj => true);
            }
        }

        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand(obj =>
                {
                    CurrentPage += 1;
                    PageChanged();
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand previousPage;
        public RelayCommand PreviousPage
        {
            get
            {
                return previousPage ??= new RelayCommand(obj =>
                {
                    CurrentPage -= 1;
                    PageChanged();
                }, obj => CurrentPage != 1);
            }
        }
        private RelayCommand firstPage;
        public RelayCommand FirstPage
        {
            get
            {
                return firstPage ??= new RelayCommand(obj =>
                {
                    CurrentPage = 1;
                    PageChanged();
                }, obj => (CurrentPage != 1));
            }
        }
        private RelayCommand lastPage;
        public RelayCommand LastPage
        {
            get
            {
                return lastPage ??= new RelayCommand(obj =>
                {
                    CurrentPage = AllPages;
                    PageChanged();
                }, obj => CurrentPage != AllPages);
            }
        }
        #endregion

        public FavoriteBooksViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
            InitFavoriteViewModel(ConnectionDB);
        }

        private async void InitFavoriteViewModel(NpgsqlConnection connection)
        {
            CurrentPage = 1;
            AllPages = await Model.DataBaseFunctions.GetBooksCountity(connection, Constants.ActiveUserId);
            PageChanged();
        }
        #region BooksFunc
        private async void PageChanged()
        {
            FavoriteBooks.Clear();
            ChangeLoadingState();
            ObservableCollection<BookCard> gettingBooks = await Model.DataBaseFunctions.GetBooksByPage(ConnectionDB, CurrentPage - 1, CountityOnPage, Constants.ActiveUserId);
            if (gettingBooks.Count > 0)
            {
                IsHasItems = true;
            }
            else { IsHasItems = false; }
            await Task.Run(
                () =>
                {
                    FavoriteBooks = gettingBooks;
                    onPropertyChanged(nameof(FavoriteBooks));
                }
                );
            await Task.Run(() => ChangeLoadingState());
        }

        private void ChangeLoadingState()
        {
            IsLoading = !IsLoading;
        }
        #endregion
    }
}
