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

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>

    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as DatePicker).SelectedDate.ToString()))
            {
                (sender as DatePicker).SelectedDate = DateTime.Now;
            }
        }
    }
}
