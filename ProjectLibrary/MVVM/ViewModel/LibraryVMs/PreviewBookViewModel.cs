using Grpc.Core;
using Grpc.Net.Client;
using Npgsql;
using ProjectLibrary.Client.Book;
using ProjectLibrary.Client.FavoriteBook;
using ProjectLibrary.Client.Order;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class PreviewBookViewModel : Core.BaseViewModel
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

        public BookFull PreviewedBook { get; set; } = new();
        private bool isOwned;

        public bool IsOwned
        {
            get { return isOwned; }
            set { isOwned = value; onPropertyChanged(nameof(IsOwned)); }
        }
        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; onPropertyChanged(nameof(IsFavorite)); }
        }
        #endregion
        #region Commands
        private RelayCommand goToPreviewGenre;
        public RelayCommand GoToPreviewGenre
        {
            get
            {
                return goToPreviewGenre ??= new RelayCommand(obj =>
                {
                    Constants.PreviousVM.Add(PreviousViewModels.BookPreviewVM);
                    LibraryNavigation.NavigateLibraryTo<PreviewGenreViewModel>(PreviewedBook.Genre.Id);
                }, obj => true);
            }
        }
        private RelayCommand goToPreviewAuthor;
        public RelayCommand GoToPreviewAuthor
        {
            get
            {
                return goToPreviewAuthor ??= new RelayCommand(obj =>
                {
                    Constants.PreviousVM.Add(PreviousViewModels.BookPreviewVM);
                    LibraryNavigation.NavigateLibraryTo<PreviewAuthorViewModel>(PreviewedBook.Author.Id);
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
        private RelayCommand changeFavoriteValue;
        public RelayCommand ChangeFavoriteValue
        {
            get
            {
                return changeFavoriteValue ??= new RelayCommand(async obj =>
                {
                    IsFavorite = !IsFavorite;
                    using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
                    var Client = new FavoriteBookService.FavoriteBookServiceClient(Channel);
                    try
                    {
                         await Client.ChangeFavoriteBookAsync(new RequestChangeFavoriteBook() { BookId = PreviewedBook.Id, Status = IsFavorite, UserId = Constants.ActiveUserId });
                    }
                    catch (RpcException ex)
                    {
                        var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                        ModalWindow.Show();
                    }
                }, obj => true);
            }
        }
        #endregion
        public PreviewBookViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
        }
        public async void GetPreviewedBook(int BookId)
        {
            HistoryCache.AppendHistoryCache(BookId, HistoryType.Book);
            await Task.Run(() => IsLoading = true);
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);

            var previewBookTask = LoadPreviewBook(Client, BookId);
            await Task.WhenAll(previewBookTask);

            onPropertyChanged(nameof(PreviewedBook));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadPreviewBook(BookService.BookServiceClient Client, int BookId)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var ClientBook = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseFullBook response = await ClientBook.GetFullBookAsync(new RequestFullBook() { BookId = BookId });
                PreviewedBook = new BookFull()
                {
                    Id = response.Id,
                    Genre = new BookFullGenre() { Id = response.Genre.Id, GenreName = response.Genre.GenreName },
                    Author = new BookFullAuthor() { Id = response.Author.Id, AuthorFullName = response.Author.AuthorFullname },
                    Description = response.Description,
                    PublicationDate = response.PublicationDate.ToDateTime(),
                    Image = response.Image.ToArray(),
                    IsBestSeller = response.IsBestseller,
                    RatingStars = response.RatingStars,
                    IsPromo = response.IsPromo,
                    Price = response.Price,
                    Title = response.Title,
                    PagesCout = response.PageCount
                };
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
            var ClientOrder = new OrderService.OrderServiceClient(Channel);
            try
            {
                ResponseIsBought response = await ClientOrder.IsBoughtAsync(new RequestIsBought() { BookId = BookId, UserId = Constants.ActiveUserId });
                IsOwned = response.IsOrdered;
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
            var ClientFavorite = new FavoriteBookService.FavoriteBookServiceClient(Channel);
            try
            {
                ResponseCheckFavoriteBook response = await ClientFavorite.CheckFavoriteBookAsync(new RequestCheckFavoriteBook() { BookId = BookId, UserId = Constants.ActiveUserId });
                IsFavorite = response.IsFavorite;
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
    }
}
