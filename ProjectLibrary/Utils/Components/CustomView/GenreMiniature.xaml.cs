using System.Windows;
using System.Windows.Controls;

namespace ProjectLibrary.Utils.Components.CustomView
{
    /// <summary>
    /// Логика взаимодействия для GenreMiniature.xaml
    /// </summary>
    public partial class GenreMiniature : UserControl
    {
        public GenreMiniature()
        {
            InitializeComponent();
        }
        public object ImageSource
        {
            get => (object)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(object), typeof(GenreMiniature));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GenreMiniature));

        public string GenreName
        {
            get { return (string)GetValue(GenreNameProperty); }
            set { SetValue(GenreNameProperty, value); }
        }
        public static readonly DependencyProperty GenreNameProperty =
            DependencyProperty.Register("GenreName", typeof(string), typeof(GenreMiniature));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(GenreMiniature));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(GenreMiniature));
    }
}
