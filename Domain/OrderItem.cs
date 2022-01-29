using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderItem
    {
        public OrderItem(OrderHeader order, int stockItemId, decimal price, string description, int quantity)
        {
            OrderHeaderId = order.Id;
            OrderHeader = order;
            StockItemId = stockItemId;
            Price = price;
            Description = description;
            Quantity = quantity; 
        }

        public int OrderHeaderId { get; private set; }
        public OrderHeader OrderHeader { get; private set; }
        public int StockItemId { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; set; }
        public decimal Total { get=> Price * Quantity; }
    }
}
