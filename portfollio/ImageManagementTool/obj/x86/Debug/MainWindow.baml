   M S B A M L   `   `   `  ����N  JImageManagementTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ImageManagementToolImageManagementTool  5   	   $    ImageManagementTool.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   ^ ZXceed.Wpf.AvalonDock, Version=2.0.2000.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4=
avalonDock,http://schemas.xceed.com/wpf/xaml/avalondock  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3local!clr-namespace:ImageManagementTool     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � DataContext 5      $   ImageManagementTool.ImageModel  5   
   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��表示��6   .@�5      m� $��TreeView��5       m� IsCheckable .  m� 	IsChecked 5      �� *	IsVisible+ �� ElementName$ TreeView��m� 5      $��	thumbnail��5       .  5      �� *	IsVisible+$ 	thumbnail��m� 5      $��ImageViewer��5       .  5      �� *	IsVisible+$ ImageViewer��5      m� 5      -   5        $��フォルダの追加��5      6(   m� 5      -   5        $��動画作成��5      6%   m� 5      -   5        $
��HELP��5      5      5      �� 5    
   $��25��6   $��Bottom=�6    
�������68   R� 5!   
   -   5         R�Name$ 	TreeView1��5!      $	��100��6�   
���������6Z   ��  �� Path$ Children� 6�   �� $
 Root���5"      �� 5#       �� ItemsSource 6+   �� $ Children�.R�5$      �� ��6    �� $ 
folderName�5%      5'      �� 5(   
   $	��Top=�6      listBoxThumbnailStyle#��[  6'   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6V   ��6{   �� *Images+�� 5)   
   $��Bottom=�6/   ��6   �� *Images/Path+6/   5+      5,      