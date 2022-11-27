
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;
internal class BlOrder:IOrder
{
    IDal DAl = new DalList();

    public BO.Order Read(int id)
    {
       
    }

    public IEnumerable<BO.OrderForList> ReadAll()
    {
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
        IEnumerable <Dal.DO.Order> allOrders= DAl.iorder.AllOrders();
        foreach (Dal.DO.Order order in allOrders)
        {
            BO.OrderForList orderForList = new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.Customer_Name;
            orderForList.AmountOfItems = 0;
            orderForList.TotalPrice = 0;
            IEnumerable<Dal.DO.OrderItem> orderItems = DAl.iorder.ProductsInOrder(order.ID);
            foreach(Dal.DO.OrderItem oi in orderItems)
            {
                orderForList.AmountOfItems += oi.Product_Amount;
                orderForList.TotalPrice += oi.Product_Price * oi.Product_Amount;
            }
            ordersForList.Add(orderForList);
        }
    }

    public BO.OrderTracking Tracking(int OrderId)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateOrderDelivery(int OrderId)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateOrderReceived(int OrderId)
    {
        throw new NotImplementedException();
    }
}
