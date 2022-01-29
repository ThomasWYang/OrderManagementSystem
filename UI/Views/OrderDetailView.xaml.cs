using Controllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetailView : Page
    {
        public OrderDetailView(OrderHeader order)
        {
            this.DataContext = order;
            InitializeComponent();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersView());
        }

        private void ProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = (OrderHeader)DataContext;
                switch (order.State)
                {
                    case OrderStates.Pending:
                        DataContext = OrderController.Instance.ProcessOrder(order.Id);
                        break;
                    case OrderStates.New:
                        DataContext = OrderController.Instance.SubmitOrder(order.Id);
                        break;
                    default:
                        throw new Exception("Invalid Order State");
                }
                //NavigationService.Navigate(new OrdersView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
