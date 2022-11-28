
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;
internal class BlOrder:IOrder
{
    IDal DAl = new DalList();
    //private BO.Order ConvertToBOorder(Dal.DO.Order DOorder)
    //{
    //    BO.Order BOorder = new BO.Order();
    //    BOorder.
    //}
    //public BO.Order Read(int id)
    //{
    //    if (id >= 0)
    //    {
    //        try
    //        {
    //            Dal.DO.Order order = DAl.iorder.Read(id);

    //        }
    //        catch (Dal.DO.NotExistExceptions err)
    //        {
    //            throw new BO.DataError(err);
    //        }
    //    }
    //}

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
            if (order.Ship_Date == DateTime.MinValue)
                orderForList.Status = (BO.eOrderStatus)0;
            else if(order.Delivery_Date==DateTime.MinValue)
                orderForList.Status = (BO.eOrderStatus)1; 
            else
                orderForList.Status= (BO.eOrderStatus)2;
            ordersForList.Add(orderForList);
        }
        return ordersForList;
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
