
using BlApi;
using BO;
using DalApi;
using System.Linq;

namespace BlImplementation;
internal class BlOrder : IOrder
{
    IDal dal = DalApi.Factory.Get();
    /// <summary>
    /// converts a do order to a bo order
    /// </summary>
    /// <param name="DOorder">order from the do</param>
    /// <returns>returns a bo order whith the datails from the given do order</returns>
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
        BOorder.Status = calculateOrderStatus((DateTime)BOorder.Ship_Date, (DateTime)BOorder.Delivery_Date);
        IEnumerable<Dal.DO.OrderItem> orderItems = dal.iorder.ProductsInOrder(DOorder.ID);
        BOorder.Items =(from DOorderitem in orderItems
                                               select new BO.OrderItem
                                               {
                                                   ID = DOorderitem.OrderItem_ID,
                                                   Name = dal.iproduct.ReadSingle(p => p.ID == DOorderitem.Product_ID).Name,
                                                   ProductID = DOorderitem.Product_ID,
                                                   Amount = DOorderitem.Product_Amount,
                                                   Price = DOorderitem.Product_Price,
                                                   TotalPrice = DOorderitem.Product_Amount * DOorderitem.Product_Price

                                               }).ToList();
        BOorder.TotalPrice = BOorder.Items.Sum(item => item.TotalPrice);
        return BOorder;
    }
    /// <summary>
    /// calculating the order status
    /// </summary>
    /// <param name="Ship_Date">the order shipping date</param>
    /// <param name="Delivery_Date">the order delivery date</param>
    /// <returns>the order status</returns>
    private BO.eOrderStatus calculateOrderStatus(DateTime? Ship_Date, DateTime? Delivery_Date)
    {
        if (Ship_Date == DateTime.MinValue)
            return BO.eOrderStatus.ORDERED;
        else if (Delivery_Date == DateTime.MinValue)
            return BO.eOrderStatus.SHIPPED;
        else
            return BO.eOrderStatus.DELIVERED;
    }
    /// <summary>
    /// read all orders from the database
    /// </summary>
    /// <returns>returns all the orders</returns>
    public IEnumerable<BO.OrderForList> ReadAll()
    {
        IEnumerable<Dal.DO.Order> allOrders = dal.iorder.ReadByFilter();
        IEnumerable<BO.OrderForList> ordersForList = from order in allOrders
                                                let orderItems = dal.iorder.ProductsInOrder(order.ID)
                                                select new BO.OrderForList
                                                {
                                                    ID = order.ID,
                                                    CustomerName = order.Customer_Name,
                                                    AmountOfItems = orderItems.Sum(oi => oi.Product_Amount),
                                                    TotalPrice = orderItems.Sum(oi => oi.Product_Amount * oi.Product_Price),
                                                    Status = calculateOrderStatus(order.Ship_Date, order.Delivery_Date),
                                                };
        return ordersForList;
    }
    /// <summary>
    /// reading a certain order by order id
    /// </summary>
    /// <param name="OrderId">order id</param>
    /// <returns>returns the order</returns>
    /// <exception cref="BO.DataError"></exception>
    /// <exception cref="BO.PropertyInValidException"></exception>
    public BO.Order Read(int OrderId)
    {
        if (OrderId >= 0)
        {
            try
            {
                Dal.DO.Order DOorder = dal.iorder.ReadSingle(p => p.ID == OrderId);
                return convertToBOorder(DOorder);
            }
            catch (Dal.DO.NotExistExceptions err)
            {
                throw new BO.DataError(err);
            }
        }
        throw new BO.PropertyInValidException("ID");
    }
    /// <summary>
    /// updating the order shipping date
    /// </summary>
    /// <param name="OrderId">the order id</param>
    /// <returns>returns the updated order</returns>
    /// <exception cref="BO.PropertyInValidException"></exception>
    /// <exception cref="BO.OrderAlreadyException"></exception>
    /// <exception cref="BO.DataError"></exception>
    public BO.Order UpdateOrderShipped(int OrderId)
    {
        if (OrderId < 0)
            throw new BO.PropertyInValidException("ID");
        try
        {
            Dal.DO.Order order = dal.iorder.ReadSingle(p => p.ID == OrderId);
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

    /// <summary>
    /// updating the order delivery date
    /// </summary>
    /// <param name="OrderId">order id</param>
    /// <returns>returns the updated order</returns>
    /// <exception cref="BO.PropertyInValidException"></exception>
    /// <exception cref="BO.OrderAlreadyException"></exception>
    /// <exception cref="BO.OrderWasNotShippedException"></exception>
    /// <exception cref="BO.DataError"></exception>

    public BO.Order UpdateOrderDelivered(int OrderId)
    {
        if (OrderId < 0)
            throw new BO.PropertyInValidException("ID");
        try
        {
            Dal.DO.Order order = dal.iorder.ReadSingle(o => o.ID == OrderId);
            if (order.Delivery_Date != DateTime.MinValue)
                throw new BO.OrderAlreadyException("delivered");
            if (order.Ship_Date == DateTime.MinValue)
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
    /// <summary>
    /// tracking a certain order
    /// </summary>
    /// <param name="OrderId">order id</param>
    /// <returns>returns the order tracking</returns>
    public BO.OrderTracking Tracking(int OrderId)
    {
        try
        {
            Dal.DO.Order order = dal.iorder.ReadSingle(o => o.ID == OrderId);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = order.ID;
            orderTracking.Status = calculateOrderStatus(order.Ship_Date, order.Delivery_Date);
            if (order.Order_Date != DateTime.MinValue)
                orderTracking.dateAndStatus.Add((order.Order_Date, BO.eOrderStatus.ORDERED));
            if (order.Ship_Date != DateTime.MinValue)
                orderTracking.dateAndStatus.Add((order.Ship_Date, BO.eOrderStatus.SHIPPED));
            if (order.Delivery_Date != DateTime.MinValue)
                orderTracking.dateAndStatus.Add((order.Delivery_Date, BO.eOrderStatus.DELIVERED));
            return orderTracking;
        }
        catch (Dal.DO.NotExistExceptions ex)
        {
            throw (new BO.DataError(ex));
        }
    }
    private void checkObjValidation(BO.Order order)
    {
        if (order.Delivery_Date != DateTime.MinValue)
            throw new OrderAlreadyException("delivered");
        double sum = order.Items.Sum(oi => oi.TotalPrice);
        if (order.TotalPrice != sum)
            throw new PropertyInValidException("total price");

        //order.Items.ForEach(oi =>
        //{
        //    if (oi.Amount < 0)
        //        throw new PropertyInValidException("amount");
        //    if (oi.Price < 0)
        //        throw new PropertyInValidException("price");
        //    if (oi.Amount > dal.iproduct.ReadSingle(p => p.ID == oi.ProductID).InStock)
        //        throw new NotInStockException(oi.Name);
        //    sum += oi.TotalPrice;
        //    });

    }

    public void Update(BO.Order order)
    {
        try
        {
            checkObjValidation(order);
            Dal.DO.Order DOorder = new();
            DOorder.ID = order.OrderID;
            DOorder.Order_Date = order.Order_Date;
            DOorder.Ship_Date = order.Ship_Date;
            DOorder.Customer_Email = order.CustomerEmail;
            DOorder.Customer_Address = order.CustomerAddress;
            DOorder.Customer_Name = order.CustomerName;
            DOorder.Delivery_Date = order.Delivery_Date;
            dal.iorder.Update(DOorder);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
