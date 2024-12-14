using FluentValidation;
using FluentValidation.Internal;
using Grpc.Core;
using Grpc.Net.Client;
using Npgsql;
using ProjectLibrary.Client.User;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Converters;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class RegViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        #region Values
        private readonly Utils.Validator _validator;
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; onPropertyChanged(nameof(FirstName)); ValidatePropertyAsync(nameof(FirstName)); }
        }
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set { secondName = value; onPropertyChanged(nameof(SecondName)); ValidatePropertyAsync(nameof(SecondName)); }
        }
        private string patronomycName;
        public string PatronomycName
        {
            get { return patronomycName; }
            set { patronomycName = value; onPropertyChanged(nameof(PatronomycName)); ValidatePropertyAsync(nameof(PatronomycName)); }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; onPropertyChanged(nameof(Login)); ValidatePropertyAsync(nameof(Login)); }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; onPropertyChanged(nameof(Mail)); ValidatePropertyAsync(nameof(Mail)); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; onPropertyChanged(nameof(Password)); ValidatePropertyAsync(nameof(Password)); }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; onPropertyChanged(nameof(ConfirmPassword)); ValidatePropertyAsync(nameof(ConfirmPassword)); }
        }
        private DateTime birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; onPropertyChanged(nameof(Birthday)); ValidatePropertyAsync(nameof(Birthday)); }
        }
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                onPropertyChanged(nameof(Navigation));
            }
        }
        #endregion
        #region Commands

        private RelayCommand backButtonToAuth;
        public RelayCommand BackButtonToAuth
        {
            get
            {
                return backButtonToAuth ??= new RelayCommand(obj =>
                {
                    Navigation.NavigateTo<AuthViewModel>();
                }, obj => true);
            }
        }
        private RelayCommand registraionCommand;
        public RelayCommand RegistraionCommand
        {
            get
            {
                return registraionCommand ??= new RelayCommand(async obj =>
                {
                    ValidateAllFields();
                    if (!HasErrors)
                    {
                        bool LoginIsUnique, EmailIsUnique;
                        LoginIsUnique = EmailIsUnique = true;
                        using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
                        var Client = new UserService.UserServiceClient(Channel);
                        try
                        {
                            ResponseCheckUnique response = await Client.CheckUniqueFieldAsync(new RequestCheckUnique { ValidStringEmail = Mail, ValidStringLogin = Login });
                            LoginIsUnique = response.LoginIsUnique;
                            EmailIsUnique = response.EmailIsUnique;
                        }
                        catch (RpcException ex)
                        {
                            var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.StatusCode}");
                            ModalWindow.Show();
                        }
                        if (LoginIsUnique && EmailIsUnique)
                        {
                            try
                            {
                                ResponseRegister response = await Client.RegisterUserAsync(new RequestRegister()
                                {
                                    FirstName = FirstName,
                                    SecondName = SecondName,
                                    PatronomycName = PatronomycName,
                                    Email = Mail,
                                    Login = Login,
                                    PasswordHash = PasswordConverters.FromPasswordToHash(Password),
                                    BirthdayDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(Birthday.ToUniversalTime()),
                                });
                                if (response.IsSuccess)
                                {
                                    FirstName = string.Empty;
                                    SecondName = string.Empty;
                                    PatronomycName = string.Empty;
                                    Mail = string.Empty;
                                    Birthday = DateTime.Now;
                                    Login = string.Empty;
                                    Password = string.Empty;
                                    ConfirmPassword = string.Empty;
                                    _errors = new();
                                    var ModalWindow = new DialogWindow("Успешная регистрация!", $"Вы попали к нам в клуб!");
                                    ModalWindow.Show();
                                    Navigation.NavigateTo<AuthViewModel>();
                                }
                            }
                            catch (RpcException ex)
                            {
                                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.StatusCode}");
                                ModalWindow.Show();
                            }
                        }
                        else
                        {
                            var ModalWindow = new DialogWindow("Ошибка!", !LoginIsUnique ? "Логин уже занят" :
                                !EmailIsUnique ? "Почта уже занята" :
                                "Логин и Почта заняты!") ;
                            ModalWindow.Show();
                        }
                    }
                    else
                    {
                        var ModalWindow = new DialogWindow("Ошибка!", $"Заполните все поля!");
                        ModalWindow.Show();
                    }
                }, obj => true);
            }
        }
        #endregion
        public RegViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            _validator = new Utils.Validator();
        }
        #region ErrorsFunctionality
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private async void ValidateAllFields()
        {
            ValidatePropertyAsync(nameof(FirstName));
            ValidatePropertyAsync(nameof(SecondName));
            ValidatePropertyAsync(nameof(PatronomycName));
            ValidatePropertyAsync(nameof(Login));
            ValidatePropertyAsync(nameof(Mail));
            ValidatePropertyAsync(nameof(Birthday));
            ValidatePropertyAsync(nameof(Password));
            ValidatePropertyAsync(nameof(ConfirmPassword));
        }
        private Dictionary<string, List<string>> _errors = new();

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChange;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChange?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private async void ValidatePropertyAsync(string propertyName)
        {
            var context = new ValidationContext<RegViewModel>(this, new PropertyChain(), new MemberNameValidatorSelector(new[] { propertyName }));
            var result = await _validator.ValidateAsync(context);
            _errors.Remove(propertyName);
            foreach (var error in result.Errors)
            {
                if (error.PropertyName == propertyName)
                {
                    if (!_errors.ContainsKey(propertyName))
                    {
                        _errors[propertyName] = new List<string>();
                    }
                    _errors[propertyName].Add(error.ErrorMessage);
                }
            }
            OnErrorsChanged(propertyName);
        }
        #endregion
    }
}
