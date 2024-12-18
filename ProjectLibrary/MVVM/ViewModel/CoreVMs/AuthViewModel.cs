using Grpc.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.User;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Converters;
using System.Windows;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class AuthViewModel : BaseViewModel
    {
        #region Values
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                onPropertyChanged();
            }
        }
        private bool isAuthorizing;

        public bool IsAuthorizing
        {
            get { return isAuthorizing; }
            set { isAuthorizing = value; onPropertyChanged(nameof(IsAuthorizing)); }
        }
        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; onPropertyChanged(nameof(Login)); }
        }
        #endregion
        #region Commands
        private RelayCommand authCommand;
        public RelayCommand AuthCommand
        {
            get
            {
                return authCommand ??= new RelayCommand(async obj =>
                {
                    string? PasswordFromSecure = PasswordConverters.GetPasswordFromSecureString(obj);
                    string? Password = PasswordFromSecure != null ? PasswordConverters.FromPasswordToHash(PasswordFromSecure) : null;
                    UserShort CurrentUser = new UserShort();
                    if (Password != null)
                    {
                        using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
                        var Client = new UserService.UserServiceClient(Channel);
                        try
                        {
                            await Task.Run(() => IsAuthorizing = true);
                            ResponseAuthorize Response = await Client.AuthorizeUserAsync(new RequestAuthorize() { Login = Login, PasswordHash = Password });
                            CurrentUser.Id = Response.Id;
                            CurrentUser.FirstName = Response.FirstName;
                            CurrentUser.SecondName = Response.SecondName;
                            CurrentUser.PatronomycName = Response.PatronomycName;
                            List<FavGenreType> FavGenres = new List<FavGenreType>();
                            FavGenres.AddRange(Response.FavoriteGenres.Select(i => new FavGenreType() { Id = i.GenreId, GenreName = i.GenreName, ClickedCountity = i.ClickedCountity }));
                            CurrentUser.ClickedGenres = FavGenres;
                            Constants.ActiveUserId = CurrentUser.Id;
                            await Task.Delay(500);
                            await Task.Run(() => IsAuthorizing = false);
                            Navigation.NavigateTo<LibraryViewModel>(CurrentUser);
                        }
                        catch (RpcException ex)
                        {
                            var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                            ModalWindow.Show();
                        }
                    }
                    else
                    {
                        var ModalWindow = new DialogWindow("Ошибка!", $"Введите пароль");
                        ModalWindow.Show();
                    }
                }, obj => true);
            }
        }
        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return registrationCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        Navigation.NavigateTo<RegViewModel>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }, obj => true);
            }
        }
        #endregion
        public AuthViewModel(INavigationService navService)
        {
            Navigation = navService;
        }
    }
}
