using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.Model;
using ProjectLibrary.MVVM.View;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace ProjectLibrary.MVVM.ViewModel
{
    class RegViewModel : Core.BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Utils.Validator _validator;
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; onPropertyChanged(); ValidateProperty(nameof(FirstName)); }
        }
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set { secondName = value; onPropertyChanged(); ValidateProperty(nameof(SecondName)); }
        }
        private string patronomycName;
        public string PatronomycName
        {
            get { return patronomycName; }
            set { patronomycName = value; onPropertyChanged(); ValidateProperty(nameof(PatronomycName)); }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; onPropertyChanged(); ValidateProperty(nameof(Login)); }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; onPropertyChanged(); ValidateProperty(nameof(Mail)); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; onPropertyChanged(); ValidateProperty(nameof(Password)); }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; onPropertyChanged(); ValidateProperty(nameof(ConfirmPassword)); }
        }
        private DateTime birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; onPropertyChanged(); ValidateProperty(nameof(Birthday)); }
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
        private RelayCommand registraionCommand;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public RelayCommand RegistraionCommand
        {
            get
            {
                return registraionCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        ValidateAllFields();
                        if (!HasErrors )
                        {
                            DateTime now = DateTime.Now;
                            User NewUser = new()
                            {
                                FirstName = FirstName,
                                SecondName = SecondName,
                                PatronomycName = PatronomycName,
                                Email = Mail,
                                BirthdayDate = Birthday,
                                Login = Login,
                                DateOfCreation = now,
                                LastUpdated = now,
                                LikedObjects = null,
                                ClickedGenres = null,
                                LastViewed = null,
                                PasswordHash = Password
                            };
                            FirstName = string.Empty;
                            SecondName = string.Empty;
                            PatronomycName = string.Empty;
                            Mail = string.Empty;
                            Birthday = DateTime.Now;
                            Login = string.Empty;
                            Password = string.Empty;
                            ConfirmPassword = string.Empty;
                            _errors = new();
                            DataBaseFunctions.AddUser(ConnectionDB, NewUser);
                            var ModalWindow = new DialogWindow("Успешная регистрация!", $"Вы попали к нам в клуб!");
                            ModalWindow.Show();
                            Navigation.NavigateTo<AuthViewModel>();
                        }
                        else
                        {
                            var ModalWindow = new DialogWindow("Ошибка!", $"Заполните все поля!");
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
        public RegViewModel(INavigationService navigation, NpgsqlConnection connection)
        {
            Navigation = navigation;
            ConnectionDB = connection;
            _validator = new Utils.Validator();
        }
        private void ValidateAllFields()
        {
            ValidateProperty(nameof(FirstName));
            ValidateProperty(nameof(SecondName));
            ValidateProperty(nameof(PatronomycName));
            ValidateProperty(nameof(Login));
            ValidateProperty(nameof(Mail));
            ValidateProperty(nameof(Birthday));
            ValidateProperty(nameof(Password));
            ValidateProperty(nameof(ConfirmPassword));
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

        private void ValidateProperty(string propertyName)
        {
            var context = new ValidationContext<RegViewModel>(this);
            var result = _validator.Validate(context);

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
    }
}
