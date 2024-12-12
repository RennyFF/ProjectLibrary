using Grpc.Core;
using Grpc.Net.Client;
using Npgsql;
using ProjectLibrary.Client.History;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class HistoryViewModel : Core.BaseViewModel
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

        public ObservableCollection<BookCardType> HistoryBooks { get; set; } = new();
        public ObservableCollection<AuthorCardType> HistoryAuthors { get; set; } = new();
        public ObservableCollection<GenreCardType> HistoryGenres { get; set; } = new();

        #endregion
        #region Commands
        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    Constants.PreviousVM = new List<PreviousViewModels?>();
                    Constants.PreviousVM.Add(PreviousViewModels.HistoryVM);
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
        #endregion
        public HistoryViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitHistoryViewModel();
        }

        private async void InitHistoryViewModel()
        {
            await Task.Run(() => IsLoading = true); 
            var loadHistoryTask = LoadHistory(HistoryCache.GetHistory());
            await Task.WhenAll(loadHistoryTask);
            onPropertyChanged(nameof(HistoryBooks));
            onPropertyChanged(nameof(HistoryAuthors));
            onPropertyChanged(nameof(HistoryGenres));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadHistory(HistoryStruct HistoryLocal)
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new HistoryService.HistoryServiceClient(Channel);
            try
            {
                ResponseHistory response = await Client.GetHistoryCardsAsync(new RequestHistory
                {
                    Authors = { HistoryLocal.authorHistory.Select(i => new AuthorHistoryReq { Id = i.AuthorId }) },
                    Books = { HistoryLocal.bookHistory.Select(i => new BookHistoryReq { Id = i.BookId }) },
                    Genres = { HistoryLocal.genreHistory.Select(i => new GenreHistoryReq { Id = i.GenreId }) },
                });
                HistoryBooks = new ObservableCollection<BookCardType>(response.Books.Select(i => new BookCardType()
                {
                    Id = i.Id,
                    AuthorFullNameShort = i.AuthorFullnameShort,
                    RatingStars = i.RatingStars,
                    Title = i.Title,
                    Image = i.Image.ToByteArray()
                }));
                HistoryAuthors = new ObservableCollection<AuthorCardType>(response.Authors.Select(i => new AuthorCardType()
                {
                    Id = i.Id,
                    FullName = i.AuthorFullname,
                    ImageAvatar = i.Image.ToByteArray(),
                }));
                HistoryGenres = new ObservableCollection<GenreCardType>(response.Genres.Select(i => new GenreCardType()
                {
                    Id = i.Id,
                    GenreName = i.GenreName,
                    ImageAvatar = i.Image.ToByteArray()
                }));
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
    }
}
