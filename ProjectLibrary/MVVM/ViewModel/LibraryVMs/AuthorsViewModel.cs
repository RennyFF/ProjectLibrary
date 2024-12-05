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
    class AuthorsViewModel : Core.BaseViewModel
    {
        #region Values
        private readonly Dictionary<int, string> SortDictionary = new() {
            { 1, "По актуальности" },
            { 2, "По алфавиту (А-Я)" },
            { 3, "По алфавиту (Я-А)" }
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

        private List<string> allSortFilter;
        public List<string> AllSortFilter
        {
            get { return allSortFilter; }
            set { allSortFilter = value; }
        }

        private string selectedSort = "По актуальности";
        public string SelectedSort
        {
            get { return selectedSort; }
            set { selectedSort = value; ChangeLoadingState(); SortAuthors(AllCards); onPropertyChanged(nameof(SelectedSort)); }
        }

        private ObservableCollection<AuthorCard> allCards = new();
        public ObservableCollection<AuthorCard> AllCards
        {
            get { return allCards; }
            set { allCards = value; }
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
                    if (obj is AuthorCard SelectedAuthor)
                    {
                        Constants.PreviousVM = new List<PreviousViewModels?>();
                        Constants.PreviousVM.Add(PreviousViewModels.AuthorsVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewAuthorViewModel>(SelectedAuthor.Id);
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

        public AuthorsViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
            InitAuthorsViewModel(connection);
        }

        private async void InitAuthorsViewModel(NpgsqlConnection connection)
        {
            CurrentPage = 1;
            AllSortFilter = new List<string>();
            foreach (var item in SortDictionary)
            {
                AllSortFilter.Add(item.Value);
            }
            AllPages = await Model.DataBaseFunctions.GetAuthorsCountity(connection);
            PageChanged();
        }
        #region AuthorsFunc
        private async void SortAuthors(ObservableCollection<AuthorCard> UnSortedList)
        {
            switch (SelectedSort)
            {
                case "По алфавиту (А-Я)":
                    AllCards = new ObservableCollection<AuthorCard>(UnSortedList.OrderBy(i => i.FullName).ToList());
                    await Task.Delay(100);
                    onPropertyChanged(nameof(AllCards));
                    break;
                case "По алфавиту (Я-А)":
                    AllCards = new ObservableCollection<AuthorCard>(UnSortedList.OrderByDescending(i => i.FullName).ToList());
                    await Task.Delay(100);
                    onPropertyChanged(nameof(AllCards));
                    break;
                case "По актуальности":
                    AllCards = new ObservableCollection<AuthorCard>(UnSortedList.OrderBy(i => i.Id).ToList());
                    await Task.Delay(100);
                    onPropertyChanged(nameof(AllCards));
                    break;
                default:
                    break;
            }
            ChangeLoadingState();
        }
        private async void PageChanged()
        {
            ChangeLoadingState();
            AllCards.Clear();
            ObservableCollection<AuthorCard> gettingBooks = await Model.DataBaseFunctions.GetAuthorsByPage(ConnectionDB, CurrentPage - 1, CountityOnPage);
            Task.Run(() => SortAuthors(gettingBooks));
        }

        private void ChangeLoadingState()
        {
            IsLoading = !IsLoading;
        }
        #endregion
    }
}
