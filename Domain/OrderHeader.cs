using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderHeader : INotifyPropertyChanged
    {
        private OrderState _state;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderHeader(int id, DateTime dateTime, int stateId)
        {
            Id = id;
            DateTime = dateTime;
            setState(stateId);
        }
        //1=New,2=Pending,3=Rejected,4=Complete
        private void setState(int stateId)
        {
            switch (stateId)
            {
                case 1:
                    _state = new OrderNew(this);            
                    break;
                case 2:
                    _state = new OrderPending(this);
                    break;
                case 3:
                    _state = new OrderRejected(this);
                    break;
                case 4:
                    _state = new OrderComplete(this);
                    break;
                default:
                    throw new InvalidOrderStateException($"Invalid State Id: {stateId}");
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
        }

        public OrderStates State
        {
            get
            {
                return _state.State;
            }
        }

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }

        public bool HasOrderItems
        {
            get
            {
                return OrderItems.Any();
            }
        }

        public ObservableCollection<OrderItem> OrderItems { get; } = new ObservableCollection<OrderItem>();

        public OrderItem AddOrUpdateOrderItem(int stockItemId,decimal price, string description, int quantity)
        {
            var item = OrderItems.FirstOrDefault(i => i.StockItemId == stockItemId);
            if(item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new OrderItem(this, stockItemId, price, description, quantity);
                OrderItems.Add(item);
            }           
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasOrderItems)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            return item;
        }

        public void RemoveOrderItem(OrderItem item)
        {
            OrderItems.Remove(item);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasOrderItems)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        }

        public void Submit()
        {
            if(!OrderItems.Any())
            {
                throw new InvalidOrderStateException("An empty order cannot be submitted");
            }
            _state.Submit(ref _state);
        }

        public void Complete()
        {
            _state.Complete(ref _state);
        }

        public void Reject()
        {
            _state.Reject(ref _state);
        }

        public decimal Total { get => OrderItems.Sum(oi=>oi.Total); } 
       

    }
}
