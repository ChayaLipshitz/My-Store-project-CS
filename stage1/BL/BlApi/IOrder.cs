
using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> ReadAll();
        public BO.Order Read(int id);
        public BO.Order UpdateOrderDelivery(int OrderId);
        public BO.Order UpdateOrderReceived(int OrderId);
        public BO.OrderTracking Tracking(int OrderId); 
    }
}
