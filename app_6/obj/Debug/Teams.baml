   M S B A M L   `   `   `  ����@  <app_6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:app_6_1app_6_1  5   	       app_6_1.Teams   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:app_6_1     5   	     � Title$  Teams��5   	   $	��450��6   $	��800��6$   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��30* �6   �� 5      $
��300* �6   �� 5      $	��40* �6    � ColumnDefinitions 5   
   �� 5      $
��100* �6   �� 5      $
��100* �6   �� 5      $
��100* �6   �� 5      $
��100* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TeamDataGrid��5      $��10q�6)   $��3��65   $��2��6I      CanUserAddRows .  6�   �� 5   
   -   5         ��Name$ Add_Team_Button��5      $��Add team��6*   $
��10,5q�6=   $��2��6K   6X   �� 5   
   -   5        $ DeliteTeam_Button��5      $��Delite team��6,   $��1��6B   $
��10,5q�6R   $��2��6`    J� RenderTransformOrigin$ 5.048,0.602&�6m   6�   �� 5   
   -   5        $ Exit_Button��5      $��Exite��6&   $��3��66   $
��10,5q�6F   $��2��6T   6a   �� 5   
   $"��Состав команды:��6   $��3��6*   $
��10,5q�6:   �� 5   
   -   5         ��Name$ TeamListBox��5      $��3��6'   $��10,10,25,15q�67   $��1��6L   5      5      