using Controllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for AddOrderItem.xaml
    /// </summary>
    public partial class AddOrderItemView : Page
    {
        private OrderHeader _order;

        public IEnumerable<StockItem> StockItems { get; private set; }

        public int Quantity { get; set; } = 1;

        public AddOrderItemView(OrderHeader order)
        {          
            InitializeComponent();
          

            try
            {
                _order = order;
                StockItems = StockItemController.Instance.GetStockItems();
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = (StockItem)dgStockItems.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Please select a stock item", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if(Quantity < 1)
            {
                MessageBox.Show("Quanity must be a positive value greater than zero", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Quantity > item.InStock)
            {
                MessageBox.Show($"There is currently not enough items in stock. Requested: {Quantity} In stock: {item.InStock}\nThis order might be rejected if there is not enough stock on hand when the order is being processed", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            try
            {
                var order = OrderController.Instance.UpsertOrderItem(_order.Id, item.Id, Quantity);
                NavigationService.Navigate(new AddOrderView(order));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
