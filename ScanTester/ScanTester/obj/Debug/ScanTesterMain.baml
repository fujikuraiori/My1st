   M S B A M L   `   `   `  ����E  AScanTester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ScanTester
ScanTester  5   	       ScanTester.ScanTesterMain   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ScanTester     5   	     � Title$  ScanTesterMain��5   	   $	��150��6    $	��350��6-    � DataContext 5	         ScanTester.ViewModel  5
   
   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� -   5        $
��load��5      $��画像フォルダ選択��6#   65   m� 5      -   5        $
��save��5      $��保存��6#   6/   5      �� 5   
   -   5         ��Name$ button��5      $��画像フォルダ選択��6!   $	��Top=�6I   ]� 5   
   $��Bottom=�6   .+�5      ]� $��Right=�6   .+�5      �� $��平均縦幅誤差 : ��6   �� 5      ��6   �� *Errors.aveHeiError+5      ]� 5      .+�5      �� $��最大縦幅誤差 : ��6   �� 5      ��6   �� *Errors.maxHeiError+5      5      ]� 5   
   $��Bottom=�6   .+�5      ]� $��Right=�6   .+�5      �� $��平均横幅誤差 : ��6   �� 5       ��6   �� *Errors.aveWidError+5!      ]� 5"      .+�5#      �� $��最大横幅誤差 : ��6   �� 5$      ��6   �� *Errors.maxWidError+5%      5&      �� 5'   
   $ �� IsSynchronizedWithCurrentItem$
 TrueY�68      ImageListTemplate#��[  6]   ��6   ��  �� Path$ MyList�6]   5(      5)      