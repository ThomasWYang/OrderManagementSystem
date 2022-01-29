using Controllers;
using Repositories;
using System.Linq;

namespace HookIn
{
    class Program
    {
        static void Main(string[] args)
        {
            var stockCtrl =  StockItemController.Instance;
            var stock = stockCtrl.GetStockItems();
            var orderCtrl = OrderController.Instance;

            var o = orderCtrl.CreateNewOrderHeader();
      
       
                o = orderCtrl.UpsertOrderItem(o.Id, stock.First().Id,1);
          
            //o = orderCtrl.SubmitOrder(o.Id);

             

        }
    }
}
