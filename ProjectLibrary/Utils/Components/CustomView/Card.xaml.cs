﻿using System.Windows;
using System.Windows.Controls;

namespace ProjectLibrary.Utils.Components.CustomView
{
    /// <summary>
    /// Логика взаимодействия для BookCard.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }
        public ILibraryNavigationService NavigationService
        {
            get => (ILibraryNavigationService)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }
        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register("NavigationService", typeof(ILibraryNavigationService), typeof(Card));
        public object ImageSource
        {
            get => (object)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(object), typeof(Card));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Card));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Card));

        public Visibility TitleVisibility
        {
            get { return (Visibility)GetValue(TitleVisibilityProperty); }
            set { SetValue(TitleVisibilityProperty, value); }
        }
        public static readonly DependencyProperty TitleVisibilityProperty =
            DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(Card));

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }
        public static readonly DependencyProperty AuthorProperty =
            DependencyProperty.Register("Author", typeof(string), typeof(Card));

        public Visibility AuthorVisibility
        {
            get { return (Visibility)GetValue(AuthorVisibilityProperty); }
            set { SetValue(AuthorVisibilityProperty, value); }
        }
        public static readonly DependencyProperty AuthorVisibilityProperty =
            DependencyProperty.Register("AuthorVisibility", typeof(Visibility), typeof(Card));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(Card));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(Card));

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register("Rating", typeof(int), typeof(Card));
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(Card));

    }
}