using Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class OrderController
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();
        private readonly StockItemRepository _stockItemRepository = new StockItemRepository();
        private static OrderController _instance; 

        private OrderController() { }
        public static OrderController Instance
        {
            get
            {                
                if (_instance == null)
                {
                     _instance = new OrderController();
                };
                return _instance;                
            }                          
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return _orderRepository.GetOrderHeaders();
        }

        public OrderHeader CreateNewOrderHeader()
        {
            var order = _orderRepository.InsertOrderHeader();
            return order;
        }

        public OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            var stockItem = _stockItemRepository.GetStockItem(stockItemId);
            var order = _orderRepository.GetOrderHeader(orderHeaderId);
            var item = order.AddOrUpdateOrderItem(stockItem.Id, stockItem.Price, stockItem.Name, quantity);
            _orderRepository.UpsertOrderItem(item);
            return order;
        }

        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            var order = _orderRepository.GetOrderHeader(orderHeaderId);
            order.Submit();
            _orderRepository.UpdateOrderState(order);
            return order;

        }

        public OrderHeader ProcessOrder(int orderHeaderId)
        {
            var order = _orderRepository.GetOrderHeader(orderHeaderId);
            try
            {
                try
                {
                    _stockItemRepository.DecrementOrderStockItemAmount(order);
                    order.Complete();
                }
                catch (SqlException ex)
                {
                    //Check Constraint Violation
                    if (ex.Number == 547)
                    {
                        order.Reject();
                    }                   
                }
                _orderRepository.UpdateOrderState(order);
            }
            catch(InvalidOrderStateException ex)
            {
                throw ex;
            }           
            return order;
        }

        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            _orderRepository.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }


        public OrderHeader DeleteOrderItem(int orderHeaderId, int stockItemId)
        {           
            _orderRepository.DeleteOrderItem(orderHeaderId, stockItemId);
            return _orderRepository.GetOrderHeader(orderHeaderId);

        }


        public void ResetOrders()
        {
            _orderRepository.ResetOrders(); 
        }
    }
}

