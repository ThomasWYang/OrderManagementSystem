   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.AddOrderItemView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5        N� Title$  Add Order Item��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $��35 �6   �� 5      $��10 �6   �� 5      $��35 �6   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   �� $K��EAdd order line item (please select an item, enter quanity and submit)��6   $��1��6a   $��1��6r   $��6��6   $��Center=�6�   $��18(�6�   $��DemiBold"�6�   �� 5    
   -   5         ��Name$ label��5       $��Quantity��6   $��2��62   $��3��6C      
LabelStyle#��[  6P   �� 5!   
   -   5         ��Name$ 	txtBoxQty��5!      $��3��6K   $��3��6[   $��Center=�6h   $��Center=�6�   $��20(�6�   $	��100��6�   ��6%   �� *
Quantity+ �� Mode$ TwoWay=�6�   �� 5"   
   -   5        $��6��5"      $��3��6!   $��Add Item��6.   6A   &   System.Windows.Controls.DataGrid  5#   
   -   5          Name$ dgStockItems��5#      $��5��6*   $��1��67   $��6��6G      AutoGenerateColumns .  $��25q�6w      
IsReadOnly . ��6�   �� *
StockItems+	   Columns	 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 SKU��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Id+6I     5&      $

 Item��6%   $ * 6L    63   �� *Name+6L     5'      $
 Price��6%   $	 150 6`    64   �� *Price+ �� StringFormat$ c��6`     5(      $
 In Stock��6%   $	 150 6T    67   �� *	InStock+6T   5*      5+      5,      