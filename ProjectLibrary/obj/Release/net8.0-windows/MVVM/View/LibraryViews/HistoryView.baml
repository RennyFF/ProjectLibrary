   M S B A M L   `   `   `  ���� G  CLibrars Cave, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:ProjectLibrary.Utils.Components.CustomView*ProjectLibrary.Utils.Components.CustomView  ]3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews%ProjectLibrary.MVVM.View.LibraryViews  Q-clr-namespace:ProjectLibrary.Utils.ConvertersProjectLibrary.Utils.Converters  7    1ProjectLibrary.MVVM.View.LibraryViews.HistoryView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  OcustomComponents8clr-namespace:ProjectLibrary.Utils.Components.CustomView  :d2http://schemas.microsoft.com/expression/blend/2008  ?local3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  9utils-clr-namespace:ProjectLibrary.Utils.Converters    � 	Resources  ?   9ProjectLibrary.Utils.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          .��� .+��� 
������ y��� *	IsLoading+ �� 	Converter# [   �� Mode$ TwoWay=�._��� $��Center=�$��Center=�$��32(�  SecondTextColorBrush#��[   HeaderTextStyle#��[ $��ЗАГРУЗКА��� y��� *	IsLoading+# [   �� ConverterParameter$ Invert��$ TwoWay=� � RowDefinitions �� $
��auto ��� .+��� $��0��$��10q�$��Center=�  MainBackgroundColorBrush#��[  �� CornerRadius$ 16��._�� $��Center=�.+��� $��
20,10,0,10q�$��Center=�$��20(�#��[ $��История���� $��1��$��	0,0,10,10q�$��False��  FavsScrollViewer#��[ $��Visible=�.���� .+��� $��0��$��	10,10,0,0q�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ Visible=�	 �� Triggers	 �� 
 �� Binding
 �� *HistoryBooks.Count+ �� Value 0.a���  � y�$ 	Collapsed=�.+��� $��0,0,0,10q�#��[ $+��%Просмотренные книги���� $
��auto��$��Center=�
������ 
������ $��0q�  ItemContainerStyle#��[ $��False��$
��Auto=�$��Disabled=� %� IsVirtualizing$
 True�� %� VirtualizationMode$ 	Recycling=����� *HistoryBooks+���� .R�A� $
��Left=� K� IsItemsHost$
 True�� A� Rows$ 1������ .R�>   8ProjectLibrary.Utils.Components.CustomView.BookMiniature     AuthorVisibility$ Visible=�   CornerRadius$ 8��   ImageHeight$	 150Q�   
ImageWidth$	 120Q�   TitleVisibility$ Visible=�   Title �� *Title+   Author �� *AuthorFullNameShort+   ImageSource �� *Image+   Rating �� *RatingStars+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ PreviewMouseLeftButtonUp��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *DataContext.GoToPreview+ �� RelativeSource ��  �� AncestorType" ��   CommandParameter �� �� $��1��$��	10,10,0,0q����� " ��.���  � y�$ Visible=�	 �� 
 �� *HistoryAuthors.Count+ 0.a���  � y�$ 	Collapsed=�.+��� $��0,0,0,10q�#��[ $-��'Просмотренные авторы���� $
��auto��$��Center=�
������ 
������ $��0q�#��[ $
��Auto=�$��Disabled=�$
 True��$ 	Recycling=����� *HistoryAuthors+���� .R�A� $
��Left=�$
 True��$ 1������ .R�@   :ProjectLibrary.Utils.Components.CustomView.AuthorMiniature      CornerRadius$  8��!   ImageHeight$	! 150Q�"   
ImageWidth$	" 150Q�#   Author# �� *
FullName+$   ImageSource$ �� *ImageAvatar+   $ PreviewMouseLeftButtonUp��.    �� *DataContext.GoToPreview+ �� " �� �� �� $��2��$��	10,10,0,0q����� " ��.���  � y�$ Visible=�	 �� 
 �� *HistoryGenres.Count+ 0.a���  � y�$ 	Collapsed=�.+��� $��0,0,0,10q�#��[ $+��%Просмотренные жанры���� $
��auto��$��Center=�
������ 
������ $��0q�#��[ $
��Auto=�$��Disabled=�$
 True��$ 	Recycling=����� *HistoryGenres+���� .R�A� $
��Left=�$
 True��$ 1������ .R�?   9ProjectLibrary.Utils.Components.CustomView.GenreMiniature  %   CornerRadius$% 8��&   ImageHeight$	& 150Q�'   
ImageWidth$	' 150Q�(   	GenreName( �� *	GenreName+)   ImageSource) �� *ImageAvatar+   $ PreviewMouseLeftButtonUp��.    �� *DataContext.GoToPreview+ �� " �� �� 