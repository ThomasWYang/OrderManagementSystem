using Controllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();
            try
            {
                dgOrders.ItemsSource = OrderController.Instance.GetOrderHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OrdersGridItem_Click(object sender, RoutedEventArgs e)
        {
           var order = (OrderHeader)((Button)e.Source).DataContext;
           NavigationService.Navigate(new OrderDetailView(order));
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderView());
        }
    }
}
