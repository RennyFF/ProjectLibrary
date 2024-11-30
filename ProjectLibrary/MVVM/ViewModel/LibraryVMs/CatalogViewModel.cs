﻿using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class CatalogViewModel : Core.BaseViewModel
    {
        #region Values
        private readonly Dictionary<int, string> SortDictionary = new() {
            { 1, "По актуальности" },
            { 2, "По алфавиту (А-Я)" },
            { 3, "По алфавиту (Я-А)" },
            { 4, "По возрастанию оценок" },
            { 5, "По убыванию оценок" },
        };
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

        private List<Genre> allGenresFilter;
        public List<Genre> AllGenresFilter
        {
            get { return allGenresFilter; }
            set { allGenresFilter = value; }
        }

        private List<string> allSortFilter;
        public List<string> AllSortFilter
        {
            get { return allSortFilter; }
            set { allSortFilter = value; }
        }

        private string selectedSort;
        public string SelectedSort
        {
            get { return selectedSort; }
            set { ChangeLoadingState(); selectedSort = value; SortBooks(AllBooks); onPropertyChanged(nameof(SelectedSort)); }
        }

        private Genre selectedGenre;
        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set { selectedGenre = value; FilterBooks(); onPropertyChanged(nameof(SelectedGenre)); }
        }

        private ObservableCollection<Cards> allBooks = new();
        public ObservableCollection<Cards> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; }
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
                    if (obj is Cards SelectedBook)
                    {
                        Constants.PreviousVM = PreviousViewModels.CatalogVM;
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.BookId);
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
        
        public CatalogViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
            InitLibraryViewModel(connection);
        }

        private async void InitLibraryViewModel(NpgsqlConnection connection)
        {
            CurrentPage = 1;

            AllGenresFilter = await Model.DataBaseFunctions.GetAllGenres(connection);
            AllGenresFilter.Insert(0, new Genre() { Id = 0, GenreName = "Все жанры" });
            SelectedGenre = AllGenresFilter.First();

            AllSortFilter = new List<string>();
            foreach (var item in SortDictionary)
            {
                AllSortFilter.Add(item.Value);
            }
            SelectedSort = AllSortFilter.First();

            AllPages = await Model.DataBaseFunctions.GetBooksCountity(connection);

            PageChanged();
        }
        #region BooksFunc
        private async void SortBooks(ObservableCollection<Cards> UnSortedList)
        {
            switch (SelectedSort)
            {
                case "По алфавиту (А-Я)":
                    AllBooks = new ObservableCollection<Cards>(UnSortedList.OrderBy(i => i.Title).ToList());
                    onPropertyChanged(nameof(AllBooks));
                    break;
                case "По алфавиту (Я-А)":
                    allBooks = new ObservableCollection<Cards>(UnSortedList.OrderByDescending(i => i.Title).ToList());
                    onPropertyChanged(nameof(AllBooks));
                    break;
                case "По актуальности":
                    AllBooks = new ObservableCollection<Cards>(UnSortedList.OrderBy(i => i.AddedInDatabase).ToList());
                    onPropertyChanged(nameof(AllBooks));
                    break;
                case "По возрастанию оценок":
                    AllBooks = new ObservableCollection<Cards>(UnSortedList.OrderBy(i => i.RatingStars).ToList());
                    onPropertyChanged(nameof(AllBooks));
                    break;
                case "По убыванию оценок":
                    AllBooks = new ObservableCollection<Cards>(UnSortedList.OrderByDescending(i => i.RatingStars).ToList());
                    onPropertyChanged(nameof(AllBooks));
                    break;
                default:
                    break;
            }
            ChangeLoadingState();
        }
        private async void FilterBooks()
        {
            CurrentPage = 1;
            if (selectedGenre.Id == 0)
            {
                AllPages = await Model.DataBaseFunctions.GetBooksCountity(ConnectionDB);
                PageChanged();
                return;
            }
            AllPages = await Model.DataBaseFunctions.GetBooksCountity(ConnectionDB, SelectedGenre);
            PageChanged(SelectedGenre);
        }

        private async void PageChanged()
        {
            AllBooks.Clear();
            ChangeLoadingState();
            ObservableCollection<Cards> gettingBooks = await Model.DataBaseFunctions.GetBooksByPage(ConnectionDB, CurrentPage - 1, CountityOnPage);
            await Task.Run(() => SortBooks(gettingBooks));
        }

        private async void PageChanged(Genre SelectedGenre)
        {
            AllBooks.Clear();
            ChangeLoadingState();
            ObservableCollection<Cards> gettingBooks = await Model.DataBaseFunctions.GetBooksByPageGenre(ConnectionDB, CurrentPage - 1, CountityOnPage, SelectedGenre);
            await Task.Run(() => SortBooks(gettingBooks));
        }

        private void ChangeLoadingState()
        {
            IsLoading = !IsLoading;
        }
        #endregion
    }
}
