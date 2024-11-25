using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Converters;
using ProjectLibrary.Utils.Types;
using System.Windows;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class AuthViewModel : BaseViewModel
    {
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
        }
        private string login = "root";

        public string Login
        {
            get { return login; }
            set { login = value; onPropertyChanged(nameof(Login)); }
        }

        private RelayCommand authCommand;
        public RelayCommand AuthCommand
        {
            get
            {
                return authCommand ??= new RelayCommand(async obj =>
                {
                    try
                    {
                        string? PasswordFromSecure = PasswordConverters.GetPasswordFromSecureString(obj);
                        string? Password = PasswordFromSecure != null ? PasswordConverters.FromPasswordToHash(PasswordFromSecure) : null;
                        User? CurrentUser = null;
                        if (Password != null)
                        {
                            CurrentUser = await Model.DataBaseFunctions.GetCurrentUser(ConnectionDB, Login, Password);
                        }
                        if (CurrentUser != null)
                        {
                           Navigation.NavigateTo<LibraryViewModel>(CurrentUser);
                        }
                        else
                        {
                            var ModalWindow = new DialogWindow("Ошибка!", $"Неверный логин или пароль!");
                            ModalWindow.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
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
        public AuthViewModel(INavigationService navService, NpgsqlConnection connection)
        {
            Navigation = navService;
            ConnectionDB = connection;
        }

    }
}
