using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class CatalogViewModel : Core.BaseViewModel
    {
        private Dictionary<int, string> SortFiltersDictionary = new Dictionary<int, string>() { 
            { 1, "По актуальности" },
            { 2, "По алфавиту" },
        };
        private int CountityOnPage = 27;
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
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
            set { selectedSort = value; onPropertyChanged(); }
        }

        private Genre selectedGenre;
        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set { selectedGenre = value; onPropertyChanged(); }
        }

        private ObservableCollection<Cards> allBooks = new();
        public ObservableCollection<Cards> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; onPropertyChanged(); }
        }

        private int allPages;
        public int AllPages
        {
            get { return allPages; }
            set { allPages = value; }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; PageChanged(); onPropertyChanged(); }
        }

        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand(obj =>
                {
                    CurrentPage += 1;
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
                }, obj => CurrentPage != AllPages);
            }
        }
        private ILibraryNavigationService _libraryNavigation;
        public ILibraryNavigationService LibraryNavigation
        {
            get => _libraryNavigation;
            set
            {
                _libraryNavigation = value;
                onPropertyChanged();
            }
        }
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
            AllGenresFilter.Insert(0, new Genre() { GenreName = "Все жанры" });
            SelectedGenre = AllGenresFilter.First();

            AllSortFilter = new List<string>();
            foreach (var item in SortFiltersDictionary)
            {
                AllSortFilter.Add(item.Value);
            }
            SelectedSort = AllSortFilter.First();

            AllPages = await Model.DataBaseFunctions.GetBooksCountity(connection);

            PageChanged();
        }

        private void SortBooks()
        {
            //if(SelectedSort == SortFiltersDictionary[2]) { AllBooks = (ObservableCollection<Cards>)AllBooks.OrderBy(i => i.Title); }
            //if(SelectedSort == SortFiltersDictionary[1]) {
            //    PageChanged();
            //}
        }
        private void FilterBooks()
        {
        }

        private async void PageChanged()
        {
            var GettingAllBooks = await Model.DataBaseFunctions.GetBooksByPage(ConnectionDB, CurrentPage - 1, CountityOnPage);
            ObservableCollection<Cards> TempBooks = new();
            foreach (var item in GettingAllBooks)
            {
                Cards newCard = new Cards() { Author = item.Author.SecondName + " " + item.Author.FirstName[0] + "." + item.Author.PatronomycName[0] + ".", Title = item.Title, ImageSource = item.Image };
                TempBooks.Add(newCard);
            }
            AllBooks = TempBooks;
        }
    }
}
