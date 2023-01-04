using Dal.DO;
using DalApi;

namespace Dal;

internal class DalOrder : Iorder
{  
    public IEnumerable<OrderItem> ProductsInOrder(int ID)
    {
        IEnumerable<OrderItem> lst = from oi in DataSource.OrderItemsList
                  let id = oi.Order_ID
                  where  id== ID
                  orderby oi.Product_ID descending
                  select oi;
        return lst; 
    }
    public int Create(Order order)
    {
        order.ID = DataSource.Config.Order_ID;
        DataSource.OrdersList.Add(order);
        return order.ID;
    }
    public bool Update(Order new_order)
    {
        int index = DataSource.OrdersList.FindIndex(o => o.ID == new_order.ID);
        if (index == -1) { throw new NotExistExceptions(); }
        DataSource.OrdersList[index] = new_order;
        return true;
    }
    public void Delete(int ID)
    {
        Order? order = DataSource.OrdersList.Where(oi => oi.ID == ID).First();
        if (order == null)
        {
            throw new NotExistExceptions();
        }
        DataSource.OrdersList.Remove((Order)order);
    }

    public IEnumerable<Order> ReadByFilter(Func<Order, bool> f = null)
    {
        if (f == null)
        {
            return DataSource.OrdersList;
        }
        return DataSource.OrdersList.Where(f);
    }
    public Order ReadSingle(Func<Order, bool> f)
    {
        IEnumerable<Order> orders = DataSource.OrdersList;
        Order order;
        try
        {
            order = orders.Where(f).First();
            return order;
        }
        catch (Exception ex)
        {
            throw new NotExistExceptions();
        }
    }
}
