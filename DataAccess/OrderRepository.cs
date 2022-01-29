using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : EntityRepository
    {
        public OrderHeader InsertOrderHeader()
        {
            OrderHeader order = null; 
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(var command = new SqlCommand("sp_InsertOrderHeader", connection))
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "SELECT * FROM OrderHeaders WHERE Id = @id";
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        reader.Read(); 
                        var dateTime = reader.GetDateTime(2);
                        order = new OrderHeader(id, dateTime, 1);
                        reader.Close(); 
                    }
                }
            }
            return order; 
        }

        public OrderHeader GetOrderHeader(int id)
        {
            OrderHeader order = null;
            using (var connection = new SqlConnection(_connectionString))
            {
               connection.Open();
                using (var command = new SqlCommand("sp_SelectOrderHeaderById @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //check if the order object has already been initialised
                            if(order == null)
                            {
                                int stateId = reader.GetInt32(1);
                                var dateTime = reader.GetDateTime(2);
                                order = new OrderHeader(id, dateTime, stateId);
                            }
                            //check if there is a order item
                            if(!reader.IsDBNull(3))
                            {
                                int stockItemId = reader.GetInt32(3);
                                string description = reader.GetString(4);
                                decimal price = reader.GetDecimal(5);
                                int quantity = reader.GetInt32(6);
                                order.AddOrUpdateOrderItem(stockItemId, price, description, quantity);
                            }                                                  
                        }
                        reader.Close(); 
                    }
                }                            
            }
            return order; 
        }


        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            var orders = new List<OrderHeader>();
            OrderHeader order = null; 
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_SelectOrderHeaders", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            if(order == null || order.Id != id)
                            {
                                int stateId = reader.GetInt32(1);
                                var dateTime = reader.GetDateTime(2);
                                order = new OrderHeader(id, dateTime, stateId);
                                orders.Add(order);
                            }
                            int stockItemId = reader.GetInt32(3);
                            string description = reader.GetString(4);
                            decimal price = reader.GetDecimal(5);
                            int quantity = reader.GetInt32(6);
                            order.AddOrUpdateOrderItem(stockItemId, price, description, quantity);
                        }
                        reader.Close();
                    }
                }
            }
            return orders;
        }

        public void UpsertOrderItem(OrderItem orderItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_UpsertOrderItem @orderHeaderId, @stockItemId, @description, @price, @quantity", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderItem.OrderHeader.Id));
                    command.Parameters.Add(new SqlParameter("@stockItemId", orderItem.StockItemId));
                    command.Parameters.Add(new SqlParameter("@description", orderItem.Description));
                    command.Parameters.Add(new SqlParameter("@price", orderItem.Price));
                    command.Parameters.Add(new SqlParameter("@quantity", orderItem.Quantity));
                    command.ExecuteNonQuery();
                }                               
            }
        }

        public void UpdateOrderState(OrderHeader order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_UpdateOrderState @orderHeaderId,@stateId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", order.Id));
                    command.Parameters.Add(new SqlParameter("@stateId", (int)order.State));
                    command.ExecuteNonQuery();
                }              
            }
        }

        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_DeleteOrderHeaderAndOrderItems @orderHeaderId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteOrderItem(int orderHeaderId,int stockItemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_DeleteOrderItem @orderHeaderId, @stockItemId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));
                    command.Parameters.Add(new SqlParameter("@stockItemId", stockItemId));
                    command.ExecuteNonQuery();
                }
            }
        }


        public void ResetOrders()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("DELETE FROM OrderItems", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM OrderHeaders";
                command.ExecuteNonQuery();
            }

        }
    }
}
