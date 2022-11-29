
using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;
internal class BlOrder:IOrder
{
    IDal dal = new DalList();
    private BO.Order convertToBOorder(Dal.DO.Order DOorder)
    {
        BO.Order BOorder = new BO.Order();
        BOorder.OrderID = DOorder.ID;
        BOorder.CustomerName = DOorder.Customer_Name;
        BOorder.CustomerAddress = DOorder.Customer_Address;
        BOorder.CustomerEmail = DOorder.Customer_Email;
        BOorder.Order_Date = DOorder.Order_Date;
        BOorder.Delivery_Date = DOorder.Delivery_Date;
        BOorder.Ship_Date = DOorder.Ship_Date;
        BOorder.Status = calculateOrderStatus(BOorder.Ship_Date, BOorder.Delivery_Date);
        BOorder.TotalPrice = 0;
        IEnumerable<Dal.DO.OrderItem> orderItems = dal.iorder.ProductsInOrder(DOorder.ID);
        foreach (Dal.DO.OrderItem DOorderitem in orderItems)
        {
            BO.OrderItem BOorderItem = new BO.OrderItem();
            BOorderItem.ID = DOorderitem.Order_ID;
            BOorderItem.Name = dal.iproduct.Read(DOorderitem.Product_ID).Name;
            BOorderItem.ProductID = DOorderitem.Product_ID;
            BOorderItem.Amount = DOorderitem.Product_Amount;
            BOorderItem.Price = DOorderitem.Product_Price;
            BOorderItem.TotalPrice = BOorderItem.Amount * BOorderItem.Price;
            BOorder.TotalPrice += BOorderItem.TotalPrice;
            BOorder.Items.Add(BOorderItem);
        }
        return BOorder;
    }
    private BO.eOrderStatus calculateOrderStatus( DateTime Ship_Date, DateTime Delivery_Date)
    {
        if (Ship_Date == DateTime.MinValue)
           return BO.eOrderStatus.ORDERED;
        else if (Delivery_Date == DateTime.MinValue)
            return BO.eOrderStatus.SHIPPED;
        else
            return BO.eOrderStatus.DELIVERED;
    }
    public IEnumerable<BO.OrderForList> ReadAll()
    {
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
        IEnumerable<Dal.DO.Order> allOrders = dal.iorder.AllOrders();
        foreach (Dal.DO.Order order in allOrders)
        {
            BO.OrderForList orderForList = new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.Customer_Name;
            orderForList.AmountOfItems = 0;
            orderForList.TotalPrice = 0;
            IEnumerable<Dal.DO.OrderItem> orderItems = dal.iorder.ProductsInOrder(order.ID);
            foreach (Dal.DO.OrderItem oi in orderItems)
            {
                orderForList.AmountOfItems += oi.Product_Amount;
                orderForList.TotalPrice += oi.Product_Price * oi.Product_Amount;
            }
            orderForList.Status = calculateOrderStatus(order.Ship_Date, order.Delivery_Date);
            ordersForList.Add(orderForList);
        }
        return ordersForList;
    }
    public BO.Order Read(int OrderId)
    {
        if (OrderId >= 0)
        {
            try
            {
                Dal.DO.Order DOorder = dal.iorder.Read(OrderId);                
                return convertToBOorder(DOorder);
            }
            catch (Dal.DO.NotExistExceptions err)
            {
                throw new BO.DataError(err);
            }
        }
        throw new BO.PropertyInValidException("ID");
    }
    public BO.Order UpdateOrderShipped(int OrderId)
    {
        if (OrderId < 0)
            throw new BO.PropertyInValidException("ID");
        try
        {
            Dal.DO.Order order = dal.iorder.Read(OrderId);
            if (order.Ship_Date != DateTime.MinValue)
                throw new BO.OrderAlreadyException("shipped");
            order.Ship_Date = DateTime.Now;
            dal.iorder.Update(order);
            return convertToBOorder(order);
        }
        catch (Dal.DO.NotExistExceptions ex)
        {
            throw new BO.DataError(ex);
        }
    }



    public BO.Order UpdateOrderDelivered(int OrderId)
    {
        if (OrderId < 0)
            throw new BO.PropertyInValidException("ID");
        try
        {
            Dal.DO.Order order = dal.iorder.Read(OrderId);
            if (order.Delivery_Date != DateTime.MinValue)
                throw new BO.OrderAlreadyException("delivered");
            if(order.Ship_Date == DateTime.MinValue)
               throw new BO.OrderWasNotShippedException();
            order.Delivery_Date = DateTime.Now;
            dal.iorder.Update(order);
            return convertToBOorder(order);
        }
        catch (Dal.DO.NotExistExceptions ex)
        {
            throw new BO.DataError(ex);
        }
    }
    public BO.OrderTracking Tracking(int OrderId)
    {
        try
        {
            Dal.DO.Order order = dal.iorder.Read(OrderId);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order.ID;
            orderTracking.Status = calculateOrderStatus(order.Ship_Date, order.Delivery_Date);
            orderTracking.dateAndStatus.Add((order.Order_Date, BO.eOrderStatus.ORDERED));
            if(order.Ship_Date != DateTime.MinValue)    
            orderTracking.dateAndStatus.Add((order.Ship_Date, BO.eOrderStatus.SHIPPED));
            if (order.Delivery_Date != DateTime.MinValue)    
            orderTracking.dateAndStatus.Add((order.Delivery_Date, BO.eOrderStatus.DELIVERED));
            return orderTracking;
        }
        catch(Dal.DO.NotExistExceptions ex)
        {
            throw(new BO.DataError(ex));
        }
    }


}
