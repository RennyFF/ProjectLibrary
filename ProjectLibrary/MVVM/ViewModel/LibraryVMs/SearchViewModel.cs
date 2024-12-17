using Grpc.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.Search;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public enum SearchType
    {
        GenreSearch,
        AuthorSearch,
        BookSearch
    }
    class SearchViewModel : Core.BaseViewModel
    {
        #region Values
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

        private string SearchString { get; set; } = string.Empty;

        private bool isHasItems = true;
        public bool IsHasItems
        {
            get { return isHasItems; }
            set { isHasItems = value; onPropertyChanged(nameof(IsHasItems)); }
        }
        private bool isContentLoading;
        public bool IsContentLoading
        {
            get { return isContentLoading; }
            set { isContentLoading = value; onPropertyChanged(nameof(IsContentLoading)); }
        }
        private bool isTypeSwitchLoading;
        public bool IsTypeSwitchLoading
        {
            get { return isTypeSwitchLoading; }
            set { isTypeSwitchLoading = value; onPropertyChanged(nameof(IsTypeSwitchLoading)); }
        }
        public int AllPages { get; set; }

        private int currentPage = 1;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; onPropertyChanged(nameof(CurrentPage)); }
        }

        private SearchType currentSearchType = SearchType.BookSearch;
        public SearchType CurrentSearchType
        {
            get { return currentSearchType; }
            set { currentSearchType = value; onPropertyChanged(nameof(CurrentSearchType)); }
        }
        public ObservableCollection<AuthorCardType> SearchResultAuthors { get; set; } = new();
        public ObservableCollection<GenreCardType> SearchResultGenres { get; set; } = new();
        public ObservableCollection<BookCardType> SearchResultBooks { get; set; } = new();
        #endregion
        #region Commands
        private RelayCommand previewResultGenres;
        public RelayCommand PreviewResultGenres
        {
            get
            {
                return previewResultGenres ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => { IsContentLoading = true; IsTypeSwitchLoading = true; });
                    await Task.Run(() => CurrentSearchType = SearchType.GenreSearch);
                    await Task.Run(() => CurrentPage = 1);
                    var changePageTask = ChangePage();
                    var loadCountityTask = LoadPagesCountity();
                    await Task.WhenAll(changePageTask, loadCountityTask);
                    UpdateDI();
                    await Task.Run(() => { IsContentLoading = false; IsTypeSwitchLoading = false; });
                }, obj => true);
            }
        }
        private RelayCommand previewResultAuthors;
        public RelayCommand PreviewResultAuthors
        {
            get
            {
                return previewResultAuthors ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => { IsContentLoading = true; IsTypeSwitchLoading = true; });
                    await Task.Run(() => CurrentSearchType = SearchType.AuthorSearch);
                    await Task.Run(() => CurrentPage = 1);
                    var changePageTask = ChangePage();
                    var loadCountityTask = LoadPagesCountity();
                    await Task.WhenAll(changePageTask, loadCountityTask);
                    UpdateDI();
                    await Task.Run(() => { IsContentLoading = false; IsTypeSwitchLoading = false; });
                }, obj => true);
            }
        }
        private RelayCommand previewResultBooks;
        public RelayCommand PreviewResultBooks
        {
            get
            {
                return previewResultBooks ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => { IsContentLoading = true; IsTypeSwitchLoading = true; });
                    await Task.Run(() => CurrentSearchType = SearchType.BookSearch);
                    await Task.Run(() => CurrentPage = 1);
                    var changePageTask = ChangePage();
                    var loadCountityTask = LoadPagesCountity();
                    await Task.WhenAll(changePageTask, loadCountityTask);
                    UpdateDI();
                    await Task.Run(() => { IsContentLoading = false; IsTypeSwitchLoading = false; });
                }, obj => true);
            }
        }
        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    Constants.PreviousVM = new List<PreviousViewModels?>();
                    Constants.PreviousVM.Add(PreviousViewModels.SearchVM);
                    if (obj is BookCardType SelectedBook)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                        return;
                    }
                    else if (obj is AuthorCardType SelectedAuthor)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewAuthorViewModel>(SelectedAuthor.Id);
                        return;
                    }
                    else if (obj is GenreCardType SelectedGenre)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewGenreViewModel>(SelectedGenre.Id);
                        return;
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
                    await Task.Run(() => UpdateDI());
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
                    await Task.Run(() => UpdateDI());
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
                    await Task.Run(() => UpdateDI());
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
                    await Task.Run(() => UpdateDI());
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        #endregion
        #region SearchViewModelFunctionality
        public async void InitSearchViewModel(string SearchString)
        {
            await Task.Run(() => this.SearchString = SearchString);
            await Task.Run(() => IsContentLoading = true);
            var loadCountityTask = LoadPagesCountity();
            var loadPageCardsTask = ChangePage();
            await Task.WhenAll(loadCountityTask, loadPageCardsTask);
            await Task.Run(() => UpdateDI());
            await Task.Run(() => IsContentLoading = false);
        }

        private void UpdateDI()
        {
            switch (CurrentSearchType)
            {
                case SearchType.GenreSearch:
                    onPropertyChanged(nameof(SearchResultGenres));
                    break;
                case SearchType.BookSearch:
                    onPropertyChanged(nameof(SearchResultBooks));
                    break;
                case SearchType.AuthorSearch:
                    onPropertyChanged(nameof(SearchResultAuthors));
                    break;
            }
        }

        private async Task LoadPagesCountity()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new SearchService.SearchServiceClient(Channel);
            if (CurrentSearchType == SearchType.BookSearch)
            {
                try
                {
                    ResSCountity response = await Client.SearchCountityBookAsync(new ReqSCountity() { CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    AllPages = response.Countity;
                    onPropertyChanged(nameof(AllPages));
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
            else if (CurrentSearchType == SearchType.AuthorSearch)
            {
                try
                {
                    ResSCountity response = await Client.SearchCountityAuthorAsync(new ReqSCountity() { CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    AllPages = response.Countity;
                    onPropertyChanged(nameof(AllPages));
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
            else if (CurrentSearchType == SearchType.GenreSearch)
            {
                try
                {
                    ResSCountity response = await Client.SearchCountityGenreAsync(new ReqSCountity() { CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    AllPages = response.Countity;
                    onPropertyChanged(nameof(AllPages));
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
        }

        private async Task ChangePage()
        {
            SearchResultAuthors.Clear();
            SearchResultBooks.Clear();
            SearchResultGenres.Clear();

            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new SearchService.SearchServiceClient(Channel);
            if (CurrentSearchType == SearchType.BookSearch)
            {
                try
                {
                    ResBooksPagination response = await Client.SearchBookPaginationAsync(new ReqPagination() { Page = CurrentPage, CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    if (response.BookCards.Count == 0)
                    {
                        IsHasItems = false;
                        return;
                    }
                    SearchResultBooks = new ObservableCollection<BookCardType>(response.BookCards.Select(i => new BookCardType()
                    {
                        Id = i.Id,
                        AuthorFullNameShort = i.AuthorFullnameShort,
                        Image = i.Image.ToByteArray(),
                        RatingStars = i.RatingStars,
                        Title = i.Title
                    }));
                    IsHasItems = true;
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
            else if (CurrentSearchType == SearchType.AuthorSearch)
            {
                try
                {
                    ResAuthorsPagination response = await Client.SearchAuthorPaginationAsync(new ReqPagination() { Page = CurrentPage, CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    if (response.AuthorCards.Count == 0)
                    {
                        IsHasItems = false;
                        return;
                    }
                    SearchResultAuthors = new ObservableCollection<AuthorCardType>(response.AuthorCards.Select(i => new AuthorCardType
                    {
                        Id = i.Id,
                        FullName = i.AuthorFullnameShort,
                        ImageAvatar = i.Image.ToByteArray(),
                    }));
                    IsHasItems = true;
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
            else if (CurrentSearchType == SearchType.GenreSearch)
            {
                try
                {
                    ResGenresPagination response = await Client.SearchGenrePaginationAsync(new ReqPagination() { Page = CurrentPage, CountityOnPage = Constants.CountityOnPage, SearchString = SearchString });
                    if (response.GenreCards.Count == 0)
                    {
                        IsHasItems = false;
                        return;
                    }
                    SearchResultGenres = new ObservableCollection<GenreCardType>(response.GenreCards.Select(i => new GenreCardType
                    {
                        Id = i.Id,
                        GenreName = i.GenreName,
                        ImageAvatar = i.Image.ToByteArray(),
                    }));
                    IsHasItems = true;
                }
                catch (RpcException ex)
                {
                    var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                    ModalWindow.Show();
                }
            }
        }
        #endregion
        public SearchViewModel(ILibraryNavigationService LibraryNavigationService)
        {
            LibraryNavigation = LibraryNavigationService;
        }
    }
}
