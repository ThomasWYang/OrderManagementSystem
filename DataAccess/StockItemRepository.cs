using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StockItemRepository : EntityRepository
    {
        public IEnumerable<StockItem> GetStockItems()
        {
            var items = new List<StockItem>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_SelectStockItems", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        int inStock = reader.GetInt32(3);
                        var item = new StockItem(id, name, price, inStock);
                        items.Add(item);
                    }
                    reader.Close();
                }
            }
            return items;
        }


        public StockItem GetStockItem(int id)
        {
            StockItem item = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_SelectStockItemById @id", connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@id", id));
                using (var reader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                {
                    reader.Read();
                    string name = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    int inStock = reader.GetInt32(3);
                    item = new StockItem(id, name, price, inStock);
                    reader.Close();
                }
            }
            return item;
        }

        public void DecrementOrderStockItemAmount(OrderHeader order)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();             
                var transaction = connection.BeginTransaction("UpdateStockAmountTransaction");
                command.Transaction = transaction;
                try
                {
                    command.CommandText = "sp_UpdateStockItemAmount @id, @amount";
                    foreach (var oi in order.OrderItems)
                    {
                        command.Parameters.Add(new SqlParameter("@id", oi.StockItemId));
                        command.Parameters.Add(new SqlParameter("@amount", -oi.Quantity));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public int ResetStockLevels(int quantity)
        {
            int result; 
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("UPDATE StockItems SET InStock = @quantity", connection))
            {
                command.Parameters.Add(new SqlParameter("@quantity", quantity));
                connection.Open();
                result = command.ExecuteNonQuery(); 
            }
            return result; 
                
        }
    }
}
