using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using ProjectLibrary.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.Book;
using Grpc.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class MainViewModel : Core.BaseViewModel
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
        private ObservableCollection<BookCardType> newBooksCategory = new();

        public ObservableCollection<BookCardType> NewBooksCategory
        {
            get { return newBooksCategory; }
            set { newBooksCategory = value; }
        }

        private ObservableCollection<BookCardType> bestSellerCategory = new();
        public ObservableCollection<BookCardType> BestSellerCategory
        {
            get { return bestSellerCategory; }
            set { bestSellerCategory = value; }
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
                        Constants.PreviousVM.Add(PreviousViewModels.MainVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                    }
                }, obj => true);
            }
        }
        private RelayCommand goToMagicBook;
        public RelayCommand GoToMagicBook
        {
            get
            {
                return goToMagicBook ??= new RelayCommand(async obj =>
                {
                    Constants.PreviousVM = new List<PreviousViewModels?>();
                    Constants.PreviousVM.Add(PreviousViewModels.MainVM);
                    LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(await GetMagicBookId());
                }, obj => true);
            }
        }
        #endregion
        #region MainViewModelFunctionality
        private async Task<int> GetMagicBookId()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);
            try
            {
                ResponseMagicBookId response = await Client.GetMagicBookIdAsync(new Google.Protobuf.WellKnownTypes.Empty());
                return response.BookId;
            }
            catch (RpcException ex)
            {
                Console.WriteLine("Status code: " + ex.Status.StatusCode);
                Console.WriteLine("Message: " + ex.Status.Detail);
            }
            return -1;
        }
        private async void InitMainViewModel()
        {
            await Task.Run(() => IsLoading = true);
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new BookService.BookServiceClient(Channel);

            var newBooksTask = LoadNewBooks(Client);
            var bestSellerTask = LoadBestSellers(Client);

            await Task.WhenAll(newBooksTask, bestSellerTask);

            onPropertyChanged(nameof(NewBooksCategory));
            onPropertyChanged(nameof(BestSellerCategory));
            await Task.Run(() => IsLoading = false);
        }

        private async Task LoadNewBooks(BookService.BookServiceClient Client)
        {
            try
            {
                ResponseNewBooks response = await Client.GetNewBooksAsync(new Google.Protobuf.WellKnownTypes.Empty());
                NewBooksCategory.Clear();
                foreach (var book in response.Books)
                {
                    NewBooksCategory.Add(new BookCardType
                    {
                        Id = book.Id,
                        Title = book.Title,
                        AddedInDatabase = book.AddedInDatabase.ToDateTime(),
                        Image = book.Image.ToByteArray(),
                        AuthorFullNameShort = book.AuthorFullnameShort,
                        RatingStars = book.RatingStars
                    });
                }
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }

        private async Task LoadBestSellers(BookService.BookServiceClient Client)
        {
            try
            {
                ResponseBestsellerBooks response = await Client.GetBestSellerBooksAsync(new Google.Protobuf.WellKnownTypes.Empty());
                BestSellerCategory.Clear();
                foreach (var book in response.Books)
                {
                    BestSellerCategory.Add(new BookCardType
                    {
                        Id = book.Id,
                        Title = book.Title,
                        AddedInDatabase = book.AddedInDatabase.ToDateTime(),
                        Image = book.Image.ToByteArray(),
                        AuthorFullNameShort = book.AuthorFullnameShort,
                        RatingStars = book.RatingStars
                    });
                }
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        #endregion
        public MainViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitMainViewModel();
        }
    }
}
