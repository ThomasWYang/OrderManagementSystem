   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.OrderDetailView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5        N� Title$  OrderDetail��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��6* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $��35 �6   �� 5      $��10 �6   �� 5      $��35 �6   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5    
   �� $��1��6   $��1��6"      
LabelStyle#��[  62   $��ORDER��6Y   �� 5!   
   $��1��6   $��2��6"     TextBoxStyle#��[ 62   ��6Y   �� *Id+ �� Mode$ OneWay=��� 5"   
   $��1��6   $��3��6"   #��[  62   $��	DATE/TIME��6Y   �� 5#   
   $��1��6   $��4��6"   #��[ 62   ��6Y   �� *
DateTime+$ OneWay=��� 5$   
   -   5         ��Name$ Orders��5$   Z   $��1��6   $��6��6!   $��Orders >��6G   6Z   �� 5%   
   $��3��6   $��1��6"   #��[  62   $��STATE��6Y   �� 5&   
   $��3��6   $��2��6"   #��[ 62   ��6Y   �� *State+$ OneWay=��� 5'   
   $��3��6   $��3��6"   #��[  62   $��TOTAL��6Y   �� 5(   
   $��3��6   $��4��6"   #��[ 62   ��6Y   �� *Total+$ OneWay=� �� StringFormat$ c���� 5+   
   -   5        $ ProcessOrder��5+      $��3��6)   $��6��66   ��5,      �� 5-       �� 
TargetType" ��6   .�5.      ��  �� Property � y� �� Value$ Hidden=�63   	 �� Triggers	 5/      �� 50      
 �� Binding
 6&   �� *State+ �� Value Pending6@   .a�51      ��  � y�$ Visible=�6;   �� 52       � ��$ Process��68   53      �� 54      
 6&   �� *State+ New6@   .a�55      ��  � y�$ Visible=�6;   �� 56       � ��$ Submit��68   57      59      5;      &   System.Windows.Controls.DataGrid  5?   
   -   5          Name$ dgOrderItems��5?      $��5��6*   $��1��67   $��6��6G      AutoGenerateColumns .     
IsReadOnly . ��6w   �� *
OrderItems+$ OneWay=�   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$	 SKU��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *StockItemId+6R     5B      $
 Item��6%   $ * 6S    63   �� *Description+6S     5C      $	 Qty��6%   $	 150 6P    62   �� *
Quantity+6P     5D      $ Price��6%   $	 150 6a    64   �� *Price+$ c��6a     5E      $ Total��6%   $	 150 6`    64   �� *Total+$ c��6`   5G      5I      5J      