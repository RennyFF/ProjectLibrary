using System.Security;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    /// 
    public interface IAuthPassword
    {
        SecureString Password { get; }
    }
    public partial class AuthView : UserControl, IAuthPassword
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
            if (!string.IsNullOrEmpty((sender as PasswordBox).Password))
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
