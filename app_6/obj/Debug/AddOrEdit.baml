   M S B A M L   `   `   `  ����@  <app_6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:app_6_1app_6_1  5   	       app_6_1.AddOrEdit   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:app_6_1     5   	   -   5          �Name$  AdorEditWindow��5   	    � Title$ 	AddOrEdit��5   	   $	��300��6   $	��300��6(   $	��300��64   $	��300��6C   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��150* �6   �� 5      $
��150* �6    � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $	��25* �6   �� 5      $	��25* �6   �� 5      $	��25* �6   �� 5      $	��25* �6   .+�5   
   �� -   5         ��Name$ ApplyButton��5      $��Apply��6&   $
��10,5q�66   $��4��6D   6Q   �� 5   
   -   5        $ 
ExitButton��5      $
��Exit��6%   $��1��64   $
��10,5q�6D   $��4��6R   6_   �� 5   
   -   5         ��Name$ 	LabelName��5      $
��Name��6#   $��0q�62   �� 5   
   -   5        $ LabelAge��5      $	��Age��6"   $��0q�60   $��1��6;   �� 5   
   -   5        $ LablePosicion��5      $��Posicion��6'   $��0q�6:   $��2��6E   �� 5   
   -   5        $ 	LableTeam��5      $
��Team��6#   $��0,0,8,0q�62   $��3��6C   �� 5   
   -   5         ��Name$ TextBoxName��5      $��1��6'   $��10q�67    �� TextWrapping$
 Wrap=�6C   �� 5   
   -	   5        $ 
TextBoxAge��5      $��1��6&   $��10q�66   $��1��6B   $
 Wrap=�6O   �� 5   
   -
   5         ��Name$ PosicionComboBox��5      $��1��6-   $��10q�6=   $��2��6I   $��35��6V   �� 5   
   -   5        $ TeamComboBox��5      $��1��6)   $��10q�69   $��3��6E   5!      5"      