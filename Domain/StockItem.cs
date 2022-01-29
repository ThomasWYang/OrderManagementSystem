using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StockItem
    {
        public StockItem(int id, string name, decimal price, int inStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InStock = inStock; 
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int InStock { get; private set; }
    }
}
