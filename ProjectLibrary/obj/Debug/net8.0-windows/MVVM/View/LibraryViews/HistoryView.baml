   M S B A M L   `   `   `  ����G  CLibrars Cave, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:ProjectLibrary.Utils.Components.CustomView*ProjectLibrary.Utils.Components.CustomView  5      ]3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews%ProjectLibrary.MVVM.View.LibraryViews  5      Q-clr-namespace:ProjectLibrary.Utils.ConvertersProjectLibrary.Utils.Converters  5
      7    1ProjectLibrary.MVVM.View.LibraryViews.HistoryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5      OcustomComponents8clr-namespace:ProjectLibrary.Utils.Components.CustomView  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      ?local3clr-namespace:ProjectLibrary.MVVM.View.LibraryViews  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      9utils-clr-namespace:ProjectLibrary.Utils.Converters  5
        � 	Resources  5      ?   9ProjectLibrary.Utils.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
   6*   5      .��5      � .+�5   
   �� 
������ y�6*   �� *	IsLoading+ �� 	Converter# [   �� Mode$ TwoWay=�._�5      �� $��Center=�5      $��Center=�5      $��32(�5        SecondTextColorBrush#��[ 5        HeaderTextStyle#��[ 5      $��ЗАГРУЗКА��5      5      � 5   
   y�6   �� *	IsLoading+# [   �� ConverterParameter$ Invert��$ TwoWay=� � RowDefinitions 5      �� 5      $
��auto �6    �� 5      .+�5       �� $��0��5!      $��10q�5"      $��Center=�5#        MainBackgroundColorBrush#��[ 5$       �� CornerRadius$ 16��5%      ._�5&      � $��Center=�6   .+�5'      �� $��
20,10,0,10q�5(      $��Center=�5)      $��20(�5*      #��[ 5+      $��История��5,      5-      5.      �� 5/      $��1��50      $��	0,0,10,10q�51      $��False��52        FavsScrollViewer#��[ 53      $��Visible=�54      .��55      �� .+�56      �� $��0��6!   $��	10,10,0,0q�6.   ��57      �� 58       �� 
TargetType" ��6$   .�59   "   ��  �� Property � y� �� Value$ Visible=�6?   	 �� Triggers	 5:   "   �� 5;   &   
 �� Binding
 62   �� *HistoryBooks.Count+ �� Value 06Y   .a�5<   *   ��  � y�$ 	Collapsed=�6G   5=   '   5?      .+�5A      �� $��0,0,0,10q�5B      #��[ 5C      $+��%Просмотренные книги��5D      �� 5E      $
��auto��5F      $��Center=�5G      
������ 
������ $��0q�5J        ItemContainerStyle#��[ 5K      $��False��5M      $
��Auto=�5N      $��Disabled=�5O       %� IsVirtualizing$
 True��5P       %� VirtualizationMode$ 	Recycling=�5Q      ��5L      �� *HistoryBooks+��5S      �� 5T   "   .R�5U   &   A� $
��Left=�5V   )    K� IsItemsHost$
 True��5W   )    A� Rows$ 1��5X   )   5Y   #   ��5\      �� 5]   "   .R�5^   &   >   8ProjectLibrary.Utils.Components.CustomView.BookMiniature     AuthorVisibility$ Visible=�5a   )      CornerRadius$ 8��5b   )      ImageHeight$	 150Q�5c   )      
ImageWidth$	 120Q�5e   )      TitleVisibility$ Visible=�5g   )      Title 5_   )   �� *Title+   Author 5`   )   �� *AuthorFullNameShort+   ImageSource 5d   )   �� *Image+   Rating 5f   )   �� *RatingStars+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5h   *   +  %Microsoft.Xaml.Behaviors.EventTrigger  5i   .      	EventName$ PreviewMouseLeftButtonUp��6=   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5j   2   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6H   �� *DataContext.GoToPreview+ �� RelativeSource ��  �� AncestorType" ��   CommandParameter 6�   �� 5k   /   5m   '   5n   #   5p      5q      �� 5r      $��1��6!   $��	10,10,0,0q�6.   ��5s      �� 5t      " ��6$   .�5u   "   ��  � y�$ Visible=�6?   	 5v   "   �� 5w   &   
 62   �� *HistoryAuthors.Count+ 06[   .a�5x   *   ��  � y�$ 	Collapsed=�6G   5y   '   5{      .+�5}      �� $��0,0,0,10q�5~      #��[ 5      $-��'Просмотренные авторы��5�      �� 5�      $
��auto��5�      $��Center=�5�      
������ 
������ $��0q�5�      #��[ 5�      $
��Auto=�5�      $��Disabled=�5�      $
 True��5�      $ 	Recycling=�5�      ��5�      �� *HistoryAuthors+��5�      �� 5�   "   .R�5�   &   A� $
��Left=�5�   )   $
 True��5�   )   $ 1��5�   )   5�   #   ��5�      �� 5�   "   .R�5�   &   @   :ProjectLibrary.Utils.Components.CustomView.AuthorMiniature      CornerRadius$  8��5�   )   !   ImageHeight$	! 150Q�5�   )   "   
ImageWidth$	" 150Q�5�   )   #   Author# 5�   )   �� *
FullName+$   ImageSource$ 5�   )   �� *ImageAvatar+ 5�   *     5�   .   $ PreviewMouseLeftButtonUp��6=   . 5�   2      6H   �� *DataContext.GoToPreview+ �� " �� 6�   �� 5�   /   5�   '   5�   #   5�      5�      �� 5�      $��2��6!   $��	10,10,0,0q�6.   ��5�      �� 5�      " ��6$   .�5�   "   ��  � y�$ Visible=�6?   	 5�   "   �� 5�   &   
 62   �� *HistoryGenres.Count+ 06Z   .a�5�   *   ��  � y�$ 	Collapsed=�6G   5�   '   5�      .+�5�      �� $��0,0,0,10q�5�      #��[ 5�      $+��%Просмотренные жанры��5�      �� 5�      $
��auto��5�      $��Center=�5�      
������ 
������ $��0q�5�      #��[ 5�      $
��Auto=�5�      $��Disabled=�5�      $
 True��5�      $ 	Recycling=�5�      ��5�      �� *HistoryGenres+��5�      �� 5�   "   .R�5�   &   A� $
��Left=�5�   )   $
 True��5�   )   $ 1��5�   )   5�   #   ��5�      �� 5�   "   .R�5�   &   ?   9ProjectLibrary.Utils.Components.CustomView.GenreMiniature  %   CornerRadius$% 8��5�   )   &   ImageHeight$	& 150Q�5�   )   '   
ImageWidth$	' 150Q�5�   )   (   	GenreName( 5�   )   �� *	GenreName+)   ImageSource) 5�   )   �� *ImageAvatar+ 5�   *     5�   .   $ PreviewMouseLeftButtonUp��6=   . 5�   2      6H   �� *DataContext.GoToPreview+ �� " �� 6�   �� 5�   /   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      