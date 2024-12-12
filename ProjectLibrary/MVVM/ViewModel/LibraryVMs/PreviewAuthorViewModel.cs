using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.Client.Book;
using Grpc.Net.Client;
using Grpc.Core;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Client.Author;
using System.Windows.Automation.Provider;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class PreviewAuthorViewModel : Core.BaseViewModel
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

        private bool isHasItems = false;
        public bool IsHasItems
        {
            get { return isHasItems; }
            set { isHasItems = value; onPropertyChanged(nameof(IsHasItems)); }
        }

        public AuthorFull PreviewedAuthor { get; set; }
        public ObservableCollection<BookCardType> AuthorsBooks { get; set; } = new();
        public int AllPages { get; set; }
        private int currentPage = 1;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; onPropertyChanged(nameof(CurrentPage)); }
        }
        #endregion
        #region Commands

        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage += 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage(PreviewedAuthor.Id);
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => onPropertyChanged(nameof(AuthorsBooks)));
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
                    var changePageTask = ChangePage(PreviewedAuthor.Id);
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => onPropertyChanged(nameof(AuthorsBooks)));
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != 1);
            }
        }

        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    if (obj is BookCardType SelectedBook)
                    {
                        Constants.PreviousVM.Add(PreviousViewModels.AuthorPreviewVM);
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
        #endregion
        public PreviewAuthorViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
        }
        public async void GetPreviewedAuthor(int AuthorId)
        {
            HistoryCache.AppendHistoryCache(AuthorId, HistoryType.Author);
            await Task.Run(() => IsLoading = true);
            var loadAuthorPreviewTask = LoadPreviewAuthor(AuthorId);
            var loadAuthorBooksTask = ChangePage(AuthorId);
            var booksCountityTask = LoadPagesCountity(AuthorId);
            await Task.WhenAll(loadAuthorBooksTask, loadAuthorPreviewTask, booksCountityTask);
            onPropertyChanged(nameof(PreviewedAuthor));
            onPropertyChanged(nameof(AuthorsBooks));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadPreviewAuthor(int AuthorId)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var ClientAuthor = new AuthorService.AuthorServiceClient(Channel);
            try
            {
                ResponseSingleAuthor response = await ClientAuthor.GetSingleAuthorAsync(new RequestSingleAuthor() { AuthorId = AuthorId });
                PreviewedAuthor = new AuthorFull()
                {
                    Id = response.Id,
                    FullName = $"{response.SecondName} {response.FirstName} {response.PatronomycName}",
                    DateOfBirth = response.DateOfBirth != null ? response.DateOfBirth.ToDateTime() : null,
                    DateOfDeath = response.DateOfDeath != null ? response.DateOfDeath.ToDateTime() : null,
                    ImageAvatar = response.Image.ToByteArray()
                };
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        private async Task LoadPagesCountity(int AuthorId)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);
            try
            {
                Client.Book.ResponseCountity response = await Client.GetCountityAsync(new Client.Book.RequestCountity()
                {
                    AuthorId = AuthorId,
                    GenreId = null,
                    CountityOnPage = Constants.CountityOnPage
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
        private async Task ChangePage(int AuthorId)
        {
            AuthorsBooks.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var ClientBook = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseBooksByAuthor response = await ClientBook.GetAuthorsBooksAsync(new RequestBooksByAuthor() { AuthorId = AuthorId, Page = CurrentPage, CountityOnPage = Constants.CountityOnPage });
                AuthorsBooks = new ObservableCollection<BookCardType>(response.Books.Select(i => new BookCardType
                {
                    Id = i.Id,
                    Title = i.Title,
                    Image = i.Image.ToByteArray(),
                    AuthorFullNameShort = i.AuthorFullnameShort,
                    RatingStars = i.RatingStars
                }));
                if (AuthorsBooks.Count > 0)
                {
                    IsHasItems = true;
                }
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
    }
}
