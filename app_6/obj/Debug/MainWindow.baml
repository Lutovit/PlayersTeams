   M S B A M L   `   `   `  ����@  <app_6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:app_6_1app_6_1  5   	       app_6_1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:app_6_1     5   	   -   5          �Name$  Form1��5   	    � Title$ PlayersList��5   	   $	��400��6   $	��800��6*   $	��400��66   $	��300��6E   .��5	      � -   5         �Name$ Grid1��5	      $��10q�6    � ColumnDefinitions 5
   
   �� 5      $
��200* �6   �� 5      $
��200* �6   �� 5      $
��200* �6   �� 5      $
��200* �6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      �� 5      $��25 �6   .+�5   
   �� -   5         ��Name$ 	AddButton��5      $��	AddPlayer��6$   $
��10,0q�68   $��2��6F   6S   �� 5   
   -   5        $ DeleteButton��5      $��Delete��6'   $��1��68   $
��10,0q�6H   $��2��6V   6c   �� 5   
   -   5        $ SaveAndExitButton��5      $
��Exit��6,   $��3��6;   $
��10,0q�6K   $��2��6Y   6f   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGridOfPlayers��5      $��10q�6.   $��1��6:   $��Stretch=�6G   $��Stretch=�6l   $��4��6�      CanUserAddRows .  6�   �� 5   
   -   5        $ AddTeamButton��5      $��Add or Edit Team��6(   $��2��6C   $
��10,0q�6S   $��2��6a   6n   5      5      