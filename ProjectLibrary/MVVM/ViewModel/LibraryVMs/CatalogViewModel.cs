using Grpc.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.Book;
using ProjectLibrary.Client.Genre;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class CatalogViewModel : Core.BaseViewModel
    {
        #region Values
        private static readonly Dictionary<int, string> SortDictionary = new() {
            { 1, "По актуальности" },
            { 2, "По алфавиту (А-Я)" },
            { 3, "По алфавиту (Я-А)" },
            { 4, "По возрастанию оценок" },
            { 5, "По убыванию оценок" },
        };
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
        private bool isContentLoading;
        public bool IsContentLoading
        {
            get { return isContentLoading; }
            set { isContentLoading = value; onPropertyChanged(nameof(IsContentLoading)); }
        }
        public ObservableCollection<GenreCardType> AllGenresFilter { get; set; } = new();
        public GenreCardType SelectedGenre { get; set; }
        public ObservableCollection<string> AllSortFilter { get; set; } = new();
        public string SelectedSort { get; set; }
        public ObservableCollection<BookCardType> AllBooks { get; set; } = new();
        public int AllPages { get; set; }

        private int currentPage = 1;
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
                    if (obj is BookCardType SelectedBook)
                    {
                        Constants.PreviousVM = new List<PreviousViewModels?>();
                        Constants.PreviousVM.Add(PreviousViewModels.CatalogVM);
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
                return nextPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage += 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand previousPage;
        public RelayCommand PreviousPage
        {
            get
            {
                return previousPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage -= 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != 1);
            }
        }
        private RelayCommand firstPage;
        public RelayCommand FirstPage
        {
            get
            {
                return firstPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage = 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => (CurrentPage != 1));
            }
        }
        private RelayCommand lastPage;
        public RelayCommand LastPage
        {
            get
            {
                return lastPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage = AllPages;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand sortChanged;
        public RelayCommand SortChanged
        {
            get
            {
                return sortChanged ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => IsContentLoading = true);
                    var sortBooksTask = SortBooks(AllBooks);
                    await Task.WhenAll(sortBooksTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => IsLoading != true);
            }
        }
        private RelayCommand filterChanged;
        public RelayCommand FilterChanged
        {
            get
            {
                return filterChanged ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => IsContentLoading = true);
                    var filterBooksTask = FilterBooks();
                    var bookCountityTask = LoadPagesCountity();
                    await Task.WhenAll(bookCountityTask, filterBooksTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => IsLoading != true);
            }
        }
        #endregion
        #region CatalogViewModelFunctionality
        private async void InitCatalogViewModel()
        {
            await Task.Run(() => IsLoading = true);
            var genreNamesTask = LoadGenres();
            var bookCountityTask = LoadPagesCountity();
            var loadMiscTask = LoadStartUp();
            var loadBooksTask = ChangePage();
            await Task.WhenAll(genreNamesTask, bookCountityTask, loadMiscTask, loadBooksTask);
            onPropertyChanged(nameof(AllGenresFilter));
            onPropertyChanged(nameof(SelectedGenre));
            onPropertyChanged(nameof(SelectedSort));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadStartUp()
        {
            AllGenresFilter.Insert(0, new GenreCardType() { Id = 0, GenreName = "Все жанры" });
            foreach (var item in SortDictionary)
            {
                AllSortFilter.Add(item.Value);
            }
            SelectedGenre = AllGenresFilter.First();
            SelectedSort = SortDictionary[1];
        }
        private async Task LoadGenres()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var GenreClient = new GenreService.GenreServiceClient(Channel);
            try
            {
                ResponseGenreNames response = await GenreClient.GetAllGenreNamesAsync(new Google.Protobuf.WellKnownTypes.Empty());
                foreach (var item in response.Genres)
                {
                    AllGenresFilter.Add(new GenreCardType()
                    {
                        GenreName = item.GenreName,
                        Id = item.Id
                    });
                }
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        private async Task LoadPagesCountity()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var BookClient = new BookService.BookServiceClient(Channel);
            try
            {
                Client.Book.ResponseCountity response = await BookClient.GetCountityAsync(new Client.Book.RequestCountity()
                {
                    CountityOnPage = Constants.CountityOnPage,
                    GenreId = SelectedGenre == null || SelectedGenre.Id == 0 ? null : SelectedGenre.Id,
                    AuthorId = null
                });
                AllPages = response.Countity;
                onPropertyChanged(nameof(AllPages));
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        private async Task ChangePage()
        {
            AllBooks.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var BookClient = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseBooksByPage response = await BookClient.GetBooksByPageAsync(new RequestBooksByPage { CountityOnPage = Constants.CountityOnPage, Page = CurrentPage, GenreId = SelectedGenre.Id == 0 || SelectedGenre == null ? null : SelectedGenre.Id });
                var UnsortedOC = new ObservableCollection<BookCardType>(response.Books.Select(i => new BookCardType
                {
                    Id = i.Id,
                    Title = i.Title,
                    Image = i.Image.ToByteArray(),
                    AuthorFullNameShort = i.AuthorFullnameShort,
                    RatingStars = i.RatingStars
                }));
                await SortBooks(UnsortedOC);
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        private async Task SortBooks(ObservableCollection<BookCardType> UnSortedList)
        {
            switch (SelectedSort)
            {
                case "По алфавиту (А-Я)":
                    AllBooks = new ObservableCollection<BookCardType>(UnSortedList.OrderBy(i => i.Title));
                    await Task.Delay(100);
                    break;
                case "По алфавиту (Я-А)":
                    AllBooks = new ObservableCollection<BookCardType>(UnSortedList.OrderByDescending(i => i.Title));
                    await Task.Delay(100);
                    break;
                case "По актуальности":
                    AllBooks = new ObservableCollection<BookCardType>(UnSortedList.OrderBy(i => i.AddedInDatabase));
                    await Task.Delay(100);
                    break;
                case "По возрастанию оценок":
                    AllBooks = new ObservableCollection<BookCardType>(UnSortedList.OrderBy(i => i.RatingStars));
                    await Task.Delay(100);
                    break;
                case "По убыванию оценок":
                    AllBooks = new ObservableCollection<BookCardType>(UnSortedList.OrderByDescending(i => i.RatingStars));
                    await Task.Delay(100);
                    break;
                default:
                    break;
            }
            onPropertyChanged(nameof(AllBooks));
        }
        private async Task FilterBooks()
        {
            CurrentPage = 1;
            await ChangePage();
        }

        #endregion
        public CatalogViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitCatalogViewModel();
        }
    }
}
