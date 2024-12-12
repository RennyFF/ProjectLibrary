using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using ProjectLibrary.Core.Types.Client;
using Grpc.Net.Client;
using ProjectLibrary.Client.Genre;
using Grpc.Core;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Client.Book;
using ResponseCountity = ProjectLibrary.Client.Book.ResponseCountity;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class PreviewGenreViewModel : Core.BaseViewModel
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
        public GenreCardType PreviewedGenre { get; set; }
        public ObservableCollection<BookCardType> GenreBooks { get; set; } = new();
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
                        Constants.PreviousVM.Add(PreviousViewModels.GenrePreviewVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                    }
                }, obj => true);
            }
        }

        private RelayCommand goBack;
        public RelayCommand GoBack
        {
            get
            {
                return goBack ??= new RelayCommand(obj =>
                {
                    BackToPreviousVM.GoToPrevious(LibraryNavigation);
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
                    var changePageTask = ChangePage(PreviewedGenre.Id);
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
                    var changePageTask = ChangePage(PreviewedGenre.Id);
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
                    var changePageTask = ChangePage(PreviewedGenre.Id);
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
                    var changePageTask = ChangePage(PreviewedGenre.Id);
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        #endregion
        public PreviewGenreViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
        }
        public async void GetPreviewedGenre(int GenreId)
        {
            HistoryCache.AppendHistoryCache(GenreId, HistoryType.Genre);
            await Task.Run(() => IsLoading = true);
            var loadGenreTask = LoadPreviewedGenre(GenreId);
            var loadBooksCountityTask = LoadBooksCountity(GenreId);
            var loadBooksTask = ChangePage(GenreId);
            await Task.WhenAll(loadGenreTask, loadBooksCountityTask, loadBooksTask);
            onPropertyChanged(nameof(PreviewedGenre));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadPreviewedGenre(int GenreId)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new GenreService.GenreServiceClient(Channel);
            try
            {
                ResponseSingleGenre response = await Client.GetSingleGenreAsync(new RequestSingleGenre() { GenreId = GenreId });
                PreviewedGenre = new GenreCardType()
                {
                    GenreName = response.GenreName,
                    Id = response.Id,
                    ImageAvatar = response.Image.ToByteArray()
                };
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        private async Task LoadBooksCountity(int GenreId)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseCountity response = await Client.GetCountityAsync(new ProjectLibrary.Client.Book.RequestCountity() { GenreId = GenreId, CountityOnPage = Constants.CountityOnPage, AuthorId = null });
                AllPages = response.Countity;
                onPropertyChanged(nameof(AllPages));
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        #region BooksFunc
        private async Task ChangePage(int GenreId)
        {
            GenreBooks.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseBooksByPage response = await Client.GetBooksByPageAsync(new RequestBooksByPage() { Page = CurrentPage, CountityOnPage = Constants.CountityOnPage, GenreId = GenreId });
                GenreBooks = new ObservableCollection<BookCardType>(response.Books.Select(i => new BookCardType
                {
                    Id = i.Id,
                    Title = i.Title,
                    Image = i.Image.ToByteArray(),
                    AuthorFullNameShort = i.AuthorFullnameShort,
                    RatingStars = i.RatingStars
                }));
                onPropertyChanged(nameof(GenreBooks));
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        #endregion
    }
}
