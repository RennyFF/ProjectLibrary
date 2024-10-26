using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLibrary.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    /// 
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
    public partial class AuthView : UserControl, IHavePassword
    {
        public SecureString Password
        {
            get
            {
                return PasswordPB.SecurePassword;
            }
        }
        public AuthView()
        {
            InitializeComponent();
        }

        private void PasswordPB_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty((sender as PasswordBox).Password))
            {
                (sender as PasswordBox).Tag = string.Empty;
            }
            else
            {
                (sender as PasswordBox).Tag = "Введите пароль";
            }
        }
    }
}
