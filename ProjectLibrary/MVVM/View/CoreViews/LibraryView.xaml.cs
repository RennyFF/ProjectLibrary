using ProjectLibrary.MVVM.ViewModel.CoreVMs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DropdownPopup.IsOpen = !DropdownPopup.IsOpen;
        }

        private void ListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is LibraryViewModel viewModel)
            {
                viewModel.ListBoxHeight = FavGenreListBox.ActualHeight;
            }
        }
    }
}
