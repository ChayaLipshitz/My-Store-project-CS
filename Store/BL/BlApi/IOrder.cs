
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> ReadAll();
        public BO.Order Read(int OrderId);
        public BO.Order UpdateOrderShipped(int OrderId);
        public BO.Order UpdateOrderDelivered(int OrderId);
        public BO.OrderTracking Tracking(int OrderId); 
        public void Update(BO.Order order);
        public BO.Order? TheNextOrderToCareFor();
        public event Action<int> inUpdate;

    }
}
