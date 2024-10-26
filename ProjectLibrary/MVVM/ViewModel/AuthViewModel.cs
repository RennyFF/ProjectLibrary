using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.View;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectLibrary.MVVM.ViewModel
{
    class AuthViewModel : Core.BaseViewModel
    {
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private RelayCommand authCommand;
        public RelayCommand AuthCommand
        {
            get
            {
                return authCommand ??= new RelayCommand(obj =>
                {
                    WritePasswordFromSecure(obj);
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
        public RelayCommand NavigateToLib { get; set; }
        public AuthViewModel(INavigationService navService, NpgsqlConnection connection)
        {
            Navigation = navService;
            ConnectionDB = connection;
             var obj = Model.DataBaseFunctions.GetCurrentUser(ConnectionDB);
            NavigateToLib = new RelayCommand(o => { Navigation.NavigateTo<LibraryViewModel>(); }, o => true);
        }
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        private void WritePasswordFromSecure(object Parameter)
        {
            var passwordContainer = Parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                Password = ConvertToUnsecureString(secureString);
            }
        }
    }
}
