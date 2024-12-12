using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using ProjectLibrary.Core.Types.Client;
using Grpc.Net.Client;
using ProjectLibrary.Client.FavoriteBook;
using Grpc.Core;
using ProjectLibrary.MVVM.View.CoreViews;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class FavoriteBooksViewModel : Core.BaseViewModel
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
        private bool isHasItems;
        public bool IsHasItems
        {
            get { return isHasItems; }
            set { isHasItems = value; onPropertyChanged(nameof(IsHasItems)); }
        }
        public ObservableCollection<BookCardType> FavoriteBooks { get; set; } = new();

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
                        Constants.PreviousVM.Add(PreviousViewModels.FavoriteVM);
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
        #endregion

        public FavoriteBooksViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitFavoriteViewModel();
        }

        private async void InitFavoriteViewModel()
        {
            await Task.Run(() => IsLoading = true);
            var bookCountityTask = LoadPagesCountity();
            var loadBooksTask = ChangePage();
            await Task.WhenAll(bookCountityTask, loadBooksTask);
            await Task.Run(() => IsLoading = false);
        }
        #region BooksFunc
        private async Task LoadPagesCountity()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new FavoriteBookService.FavoriteBookServiceClient(Channel);
            try
            {
                ResponseCountity response = await Client.GetCountityAsync(new RequestCountity() { CountityOnPage = Constants.CountityOnPage, UserId = Constants.ActiveUserId});
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
            FavoriteBooks.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new FavoriteBookService.FavoriteBookServiceClient(Channel);
            try
            {
                ResponseFavoriteBookByUser response = await Client.GetFavoriteBooksByUserAsync(new RequestFavoriteBookByUser() { CountityOnPage = Constants.CountityOnPage, Page = CurrentPage, UserId = Constants.ActiveUserId});
                FavoriteBooks = new ObservableCollection<BookCardType>(response.FavoriteBooks.Select(i => new BookCardType
                {
                    Id = i.Id,
                    Title = i.Title,
                    AddedInDatabase = i.AddedInDatabase.ToDateTime(),
                    Image = i.Image.ToByteArray(),
                    AuthorFullNameShort = i.AuthorFullnameShort,
                    RatingStars = i.RatingStars
                }));
                if (FavoriteBooks.Count > 0)
                {
                    IsHasItems = true;
                }
                else { IsHasItems = false; }
                onPropertyChanged(nameof(FavoriteBooks));
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
