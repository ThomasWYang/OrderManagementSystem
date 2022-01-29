using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class OrderHeaderTests
    {

        [TestInitialize]
        public void Initialize()
        {
            OrderController.Instance.ResetOrders();
            StockItemController.Instance.ResetStockLevels(10);
        }

        [TestCleanup]
        public void Cleanup()
        {
            OrderController.Instance.ResetOrders();
            StockItemController.Instance.ResetStockLevels(10);
        }

        [TestMethod]
        public void CreateNewOrderHeader_ShouldHaveIdGenerated()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.Id > 0);
        }

        [TestMethod]
        public void CreateNewOrderHeader_ShouldHaveDateTime()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.DateTime != null);
        }

        [TestMethod]
        public void CreateNewOrderHeader_OrderStateShouldBeNew()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.State == OrderStates.New);
        }


        [TestMethod]
        public void GetOrderHeaders_ShouldGetOrders()
        {
            var stock = StockItemController.Instance.GetStockItems().First();
            var orders = new List<OrderHeader>(); 
            for(int i = 0; i < 10;i++)
            {
                var o = OrderController.Instance.CreateNewOrderHeader();
                o = OrderController.Instance.UpsertOrderItem(o.Id, stock.Id, 1);
                orders.Add(o);
            }
            Assert.AreEqual(orders.Count, OrderController.Instance.GetOrderHeaders().Count());
        }

        [TestMethod]
        public void DeleteOrder_ShouldNotExist()
        {   
            var o = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            o = OrderController.Instance.UpsertOrderItem(o.Id, stock.Id, 1);
            OrderController.Instance.DeleteOrderHeaderAndOrderItems(o.Id);
            var order = OrderController.Instance.GetOrderHeaders().FirstOrDefault(oh => oh.Id == o.Id);
            Assert.IsNull(order);
        }


        [TestMethod]
        public void AddOrderItem_ShouldCreateOrderItem()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 1);
            var item = order.OrderItems.FirstOrDefault(i => i.StockItemId == stock.Id);
            Assert.IsNotNull(item);
            Assert.AreEqual(order.Id, item.OrderHeaderId);
        }

        [TestMethod]
        public void AddOrderItem_OrderShouldHaveOrderItems()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 1);
            Assert.IsTrue(order.HasOrderItems);
        }

        [TestMethod]
        public void AddOrderItem_OrderItemTotalShouldBeUpdatedCorrectly()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            int quantity = 3;          
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, quantity);
            var item = order.OrderItems.First(i => i.StockItemId == stock.Id);
            decimal lineItemTotal = stock.Price * quantity;
            Assert.AreEqual(lineItemTotal, item.Total);
        }


        [TestMethod]
        public void AddOrderItem_AddingDuplicationStockItemShouldIncrementQuantity()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            int quantity = 3;
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, quantity);
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, quantity);
            var item = order.OrderItems.First(i => i.StockItemId == stock.Id);        
            Assert.AreEqual(quantity * 2, item.Quantity);
        }


        [TestMethod]
        public void AddOrderItem_OrderTotalShouldBeUpdatedCorrectly()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().Take(5).ToList();
            int quantity = 2;
            decimal total = 0.0m;
            foreach (var s in stock)
            {
                total += s.Price * quantity;
                order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, quantity);

            }
            Assert.AreEqual(total, order.Total);
        }


        [TestMethod]
        public void RemoveOrderItem_OrderTotalShouldBeUpdatedCorrectly()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().Take(5).ToList();
            foreach (var s in stock)
            {
                order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, 1);

            }
            decimal total = order.Total;
            foreach(var i in order.OrderItems)
            {
                total -= i.Price * i.Quantity;
                order = OrderController.Instance.DeleteOrderItem(order.Id, i.StockItemId);
                Assert.AreEqual(order.Total, total);
            }
        }

        [TestMethod]
        public void RemoveOrderItem()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 1);
            var i1 = order.OrderItems.First(i => i.StockItemId == stock.Id);
            order.RemoveOrderItem(i1);
            var i2 = order.OrderItems.FirstOrDefault(i => i.StockItemId == stock.Id);
            Assert.IsNull(i2);
        }


        [TestMethod]
        public void SubmitOrder_OrderWithNoOrderItemsCannotBeSubmitted()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            try
            {
                OrderController.Instance.SubmitOrder(order.Id);
               
            }
            catch(InvalidOrderStateException){}
        }

        [TestMethod]
        public void SubmitOrder_OrderStateShouldChangeToPending()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.AreEqual(OrderStates.New, order.State);
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 3);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
        }

        [TestMethod]
        public void SubmitOrder_CannotSubmitPendingOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 1);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);

            try
            {
                OrderController.Instance.SubmitOrder(order.Id);
               
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }


        [TestMethod]
        public void SubmitOrder_CannotSubmitCompleteOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Complete, order.State);
            try
            {
                OrderController.Instance.SubmitOrder(order.Id);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }

        [TestMethod]
        public void SubmitOrder_CannotSubmitRejectedOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock +1);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Rejected, order.State);
            try
            {
                OrderController.Instance.SubmitOrder(order.Id);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }

        [TestMethod]
        public void ProcessOrder_OrderStateShouldChangeToComplete()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems(); 
            foreach(var s in stock)
            {
                order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, 2);
            }
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Complete, order.State);
        }


        [TestMethod]
        public void ProcessOrder_StockLevelShouldBeCorrectlyDecremented()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems();
            foreach (var s in stock)
            {
                order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, s.InStock);
            }
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Complete, order.State);
            stock = StockItemController.Instance.GetStockItems();
            foreach(var s in stock)
            {
                Assert.AreEqual(0, s.InStock);
            }

        }

        [TestMethod]
        public void ProcessOrder_OrderStateShouldChangeToRejected()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems();
            foreach (var s in stock)
            {
                order = OrderController.Instance.UpsertOrderItem(order.Id, s.Id, s.InStock +1);
            }
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Rejected, order.State);
        }


       

        [TestMethod]
        public void ProcessOrder_CannotCompleteNewOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock);
            Assert.AreEqual(OrderStates.New, order.State);
            try
            {
                OrderController.Instance.ProcessOrder(order.Id);
               
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }

        [TestMethod]
        public void ProcessOrder_CannotRejectNewOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock+1);
            Assert.AreEqual(OrderStates.New, order.State);
            try
            {
                OrderController.Instance.ProcessOrder(order.Id);
               
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }


        [TestMethod]
        public void ProcessOrder_CannotCompleteCompletedOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, 1);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Complete, order.State);
            try
            {
                OrderController.Instance.ProcessOrder(order.Id);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }

        [TestMethod]
        public void ProcessOrder_CannotCompleteRejectedOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock + 1);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Rejected, order.State);
            try
            {
                order.Complete();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }


        [TestMethod]
        public void ProcessOrder_CannotRejecteRejectedOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id, stock.InStock + 1);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Rejected, order.State);
            try
            {
                order.Reject();
               
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }


        [TestMethod]
        public void ProcessOrder_CannotRejectCompletedOrder()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            var stock = StockItemController.Instance.GetStockItems().First();
            order = OrderController.Instance.UpsertOrderItem(order.Id, stock.Id,1);
            Assert.AreEqual(OrderStates.New, order.State);
            order = OrderController.Instance.SubmitOrder(order.Id);
            Assert.AreEqual(OrderStates.Pending, order.State);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Complete, order.State);
            try
            {
                order.Reject();
               
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }


        [TestMethod]
        public void CreateOrder_InvalidState()
        {
            try
            {
                var order = new OrderHeader(1, DateTime.Now, 20);
                Assert.Fail("Shouldn't get here");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOrderStateException));
            }
        }








    }
}
