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

namespace ProjectLibrary.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
            //Max 3 items
            var items = new List<Item>();

            items.Add(new Item
            {
                Name = "Роман",
            });
            items.Add(new Item
            {
                Name = "Роман",
            });
            items.Add(new Item
            {
                Name = "Детские приключенческие книги",
            });
            GenreBox.ItemsSource = items;
        }

        public class Item
        {
            public string Name { get; set; }
            public int RandomWidth { get; set; }
        }
    }
}
