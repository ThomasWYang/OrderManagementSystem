   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.AddOrderView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5        N� Title$  AddOrder��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $	��33* �6   �� 5      $	��33* �6   �� 5      $	��15* �6   �� 5      $
��341* �6   �� 5      $��10 �6   .+�5   
   �� $��ORDER��6   $��1��6    $��1��6-      
LabelStyle#��[  6=   �� 5   
   $��1��63   $��2��6@     TextBoxStyle#��[ 6Q   ��6   �� *Id+ �� Mode$ OneWay=�6Q   �� 5    
   $��	DATE/TIME��6   $��1��6$   $��3��61   #��[  6A   �� 5!   
   $��1��69   $��4��6F   #��[ 6V   ��6   �� *
DateTime+$ OneWay=�6V   �� 5"   
   $��TOTAL��6   $��1��6    $��5��6-   #��[  6=   �� 5#   
   $��1��6H   $��6��6U   #��[ 6e   ��6   �� *Total+$ OneWay=� �� StringFormat$ c��6e   �� 5$   
   $��STATE��6   $��2��6    $��1��6-   #��[  6=   $��30��6a   $��55��6m   �� 5%   
   $��2��66   $��2��6C   #��[ 6S   ��6   �� *State+$ OneWay=�6S   �� 5(   
   -   5         ��Name$ AddItem��5(      $��2��6#   $��4��60   $��Add Item��6@   $��5q�6S   $��5,0,0,0q�6u   �� 5)   
   -   5        $ Submit��5)      $��2��6"   $��5��6/   $��Submit��6?   $��5q�6P   $��5,0,0,0q�6q   {�6�   �� *HasOrderItems+$ OneWay=��� 5*   
   -   5        $ Cancel��5*      $��2��6!   $��6��6.   $��Cancel��6>   $��5q�6O   $��5,0,0,0q�6p   &   System.Windows.Controls.DataGrid  5-   
   -   5          Name$ dgAddOrderItems��5-      $��4��6-   $��1��6:   $��6��6J      AutoGenerateColumns .  $��25q�6z      
IsReadOnly . ��6�   �� *
OrderItems+$ OneWay=�	   Columns	 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 SKU��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 75 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *StockItemId+6R     50      $

 Item��6%   $ * 6S    63   �� *Description+6S     51      $	
 Qty��6%   $ 75 6P    62   �� *
Quantity+6P     52      $
 Price��6%   $ 75 6`    64   �� *Price+$ c��6`     53      $
 Total��6%   $ 75 6a    64   �� *Total+$ c��6a   4  .System.Windows.Controls.DataGridTemplateColumn  54      $	 100 6)      CellTemplate 55      �� 56      .R�57      �� $ DeleteOrderItem��6%   -   5        $��0q�57   \   
�������
���  ��.��6�   Delete6�   58      5:      5<      5=      5>      