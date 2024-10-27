using ProjectLibrary.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectLibrary.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(string HeaderMessage, string MainMessage)
        {
            InitializeComponent();
            DataContext = new DialogWindowViewModel(this, HeaderMessage, MainMessage);
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
