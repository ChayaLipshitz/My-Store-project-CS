
using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> ReadAll();
        public Order Read(int id);
        public Order UpdateShipping(int OrderId);
        public Order UpdateReceived(int OrderId);
        public OrderTracking Tracking(int OrderId); 
    }
}
