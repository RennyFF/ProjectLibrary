   M S B A M L   `   `   `  ���� G  CLibrars Cave, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ProjectLibrary.Utils.ComponentsProjectLibrary.Utils.Components  c6clr-namespace:ProjectLibrary.MVVM.ViewModel.LibraryVMs(ProjectLibrary.MVVM.ViewModel.LibraryVMs  Q-clr-namespace:ProjectLibrary.Utils.ComponentsProjectLibrary.Utils.Components  ]3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews%ProjectLibrary.MVVM.View.LibraryViews  Q-clr-namespace:ProjectLibrary.Utils.ConvertersProjectLibrary.Utils.Converters  ;    5ProjectLibrary.MVVM.View.LibraryViews.PreviewBookView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     JcustomComponents-clr-namespace:ProjectLibrary.Utils.Components     @d2http://schemas.microsoft.com/expression/blend/2008     ^ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj	 fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag
 cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al  hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai! eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al" hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai# eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah$ dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai% eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj& fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag' cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af( bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj) fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak* gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah+ dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks&    	 
                        ! " # $ % & ' ( ) * + �
libraryvms6clr-namespace:ProjectLibrary.MVVM.ViewModel.LibraryVMs&    	 
                        ! " # $ % & ' ( ) * + �loading-clr-namespace:ProjectLibrary.Utils.Components&    	 
                        ! " # $ % & ' ( ) * + �local3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews&    	 
                        ! " # $ % & ' ( ) * + �mc;http://schemas.openxmlformats.org/markup-compatibility/2006&    	 
                        ! " # $ % & ' ( ) * + �utils-clr-namespace:ProjectLibrary.Utils.Converters&    	 
                        ! " # $ % & ' ( ) * +   � 	Resources  ?   9ProjectLibrary.Utils.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter?   9ProjectLibrary.Utils.Converters.ByteArrayToImageConverter  ByteArrayToImageConverter��%   &	        &	          .��� .+��� 
������ y��� *	IsLoading+ �� 	Converter# [   �� Mode$ TwoWay=�._��� $��Center=�$��Center=�$��32(�  SecondTextColorBrush#��[   HeaderTextStyle#��[ $��ЗАГРУЗКА��� $��10q�y��� *	IsLoading+# [   �� ConverterParameter$ Invert��$ TwoWay=� � ColumnDefinitions �� $
��0.4* ���  � RowDefinitions �� $��* ��� $	��200 ��� $��40 �.+��� $��0��$��	0,0,20,20q� �� CornerRadius$ 8������  �� BitmapScalingMode$ HighQuality=� j� Stretch$ UniformToFill=�	 �� ImageSource	 �� *PreviewedBook.Image+# [ �� $��2��$��1��._��  �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$��0,0,0,20q�
 �� Orientation$
 
Horizontal=�.+��� $��0,0,10,0q� �� Padding$
 10,5q�$
��Left=�
��� ���$ 12��y��� *PreviewedBook.IsBestSeller+# [  $ TwoWay=�._��� $
��Bold"�  SimpleTextStyleWhite#��[ $��Бестселлер���� $��0,0,10,0q�$
 10,5q�$
��Left=�
��� ���$ 12��._��� $
��Bold"�#��[ $!��Выбор редакции���� $��0,0,10,0q�$
 10,5q�$
��Left=�
��� ���$ 12��y��� *PreviewedBook.IsPromo+# [  $ TwoWay=�._��� $
��Bold"�#��[ $��Промо-акция���� $��1��$��0,0,0,10q�$��26(�#��[ $��CharacterEllipsis=����� *PreviewedBook.Title+�� $��2��$��0,0,0,40q�$
��Left=�$��Center=� � Cursor$
 Hand��$��20(�  MainTextColorBrush#��[   LinkText#��[ ���� *GoToPreviewAuthor+���� ���� *%#PreviewedBook.Author.AuthorFullName+�� $��3��$��0,0,0,10q�$��Center=�$
 
Horizontal=�.+��� $��0,0,10,0q�$��18(�#��[ #��[ $��Рейтинг:��1   +ProjectLibrary.Utils.Components.RatingStars  $
��Left=�   Stars �� *PreviewedBook.RatingStars+�� $��4��$��0,0,0,10q�$��Bottom=�$
 
Horizontal=�.+��� $��0,0,10,0q�$��18(�#��[ $��	Жанр:���� $
 Hand��$��16(�#��[ #��[ ���� *GoToPreviewGenre+���� ���� *PreviewedBook.Genre.GenreName+�� $��5��$��0,0,0,10q�$��Center=�#��[ �� $��18(�#��[ $�Дата издания:�� �� $��16(���� *PreviewedBook.PublicationDate+ �� StringFormat$ 
dd.MM.yyyy���� $��6��$��0,0,0,10q�$��Center=�#��[ �� $��18(�#��[ $*�$Количество страниц:�� �� $��16(���� *PreviewedBook.PagesCout+�� $��7��#��[ $
��Wrap=��� $��18(�#��[ $�Описание:�� �� $��16(���� *PreviewedBook.Description+�� $��2��$��2��$��Center=�$��Center=�$
 
Horizontal=�.+��� $	��220��$��40��$��0q�$��16(�  ButtonStyle#��[ y��� *	IsOwned+# [  $ TwoWay=�.���� $��Center=�$
 
Horizontal=�.+��� $��10q�$��Center=�$��Читать��. 
 (MahApps.Metro.IconPacks.PackIconCodicons  $��24��$��24��$��10,0,0,0q�$��Center=�   Kind$
 Book=� J� RenderTransformOrigin$ -0.21,1.877&��� y��� *PreviewedBook.IsPromo+# [  $ TwoWay=�.+��� $	��220��$��40��$��	10,0,10,0q�$��0q�$��16(�#��[ .���� $��Center=�$
 
Horizontal=�.+��� $��10q�$��Center=�$'��!Забрать бесплатно��,  &MahApps.Metro.IconPacks.PackIconLucide  $��24��$��24��$��10,0,0,0q�$��Center=�   Kind$ SquareMousePointer=�$ -0.21,1.877&� � ToolTip _� $0��*Волшебная книга месяца��  ToolTipStyle#��[ �� $	��220��$��40��$��0q�$��16(�#��[ y��� *	IsOwned+# [  $ Invert��$ TwoWay=�.���� $��Center=�$
 
Horizontal=�.+��� $��10q�$��Center=��� $�Купить�� �� ��� *PreviewedBook.Price+ �� $	�₽��3   -MahApps.Metro.IconPacks.PackIconPhosphorIcons  $��24��$��24��$��10,0,0,0q�$��Center=�   Kind$ ShoppingBagOpen=�$ -0.21,1.877&��� $��2��$��2��$��40��$��40��$
��Left=�$
 Hand��#��[ ���� *GoBack+.��1  +MahApps.Metro.IconPacks.PackIconFontAwesome  $��5q�   Kind$ ArrowLeftSolid=��� $��2��$��2��$��40��$��40��$��Right=�$
 Hand��#��[ ���� *ChangeFavoriteValue+.��� .+�  $��5q�$ StarRegular=�y��� *
IsFavorite+# [  $ Invert��$ TwoWay=�  $��5q�$ 	StarSolid=�y��� *
IsFavorite+# [  $ TwoWay=�