   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.OrdersView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5        N� Title$  Orders��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��10.0 �6   �� 5      $��* �6   �� 5      $
��10.0 �6    � RowDefinitions 5   
   �� 5      $
��10.0 �6   �� 5      $
��50.0 �6   �� 5      $
��10.0 �6   �� 5      $��* �6   �� 5      $
��10.0 �6   .+�5   
   �� -   5         ��Name$ AddOrder��5      $��	Add Order��6$   $��1��6O   $��1��6\   $	��250��6l   $��Right=�6x   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgOrders��5      $��1��6&   $��3��66      AutoGenerateColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 100* 6H   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *Id+6H     5      $ DateTime��6%   $
	 150* 6U   
 67   �� *
DateTime+6U     5      $ Items��6%   $
	 100* 6Z   
 64   �� *OrderItems.Count+6Z     5      $ Total��6%   $
	 150* 6`   
 64   �� *Total+ �� StringFormat$ c��6`     5      $ State��6%   $
	 150* 6O   
 64   �� *State+6O   4  .System.Windows.Controls.DataGridTemplateColumn  5       $
	 250* 6)   $ Details��66      CellTemplate 5!      �� 5"      .R�5#      �� -   5        .��5#   C   	Details6L   5$      5&      5(      5)      5*      