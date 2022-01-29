using Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class StockItemController
    {
        private readonly StockItemRepository _stockItemRepository = new StockItemRepository();

        private static StockItemController _instance;

        private StockItemController() { }
        public static StockItemController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockItemController();
                }
                return _instance;
            }
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            return _stockItemRepository.GetStockItems(); 
        }


        public void ResetStockLevels(int quantity)
        {
            _stockItemRepository.ResetStockLevels(quantity); 
        }
    }
}
