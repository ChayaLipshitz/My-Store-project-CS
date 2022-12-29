using Dal.DO;
using DalApi;


namespace Dal;

internal class DalOrderItem : IorderItem
{
    public int Create(OrderItem obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem> ReadByFilter(Func<OrderItem, bool> f = null)
    {
        throw new NotImplementedException();
    }

    public OrderItem ReadSingle(Func<OrderItem, bool> f)
    {
        throw new NotImplementedException();
    }

    public OrderItem Read_item_by_product_order(int order_id, int product_id)
    {
        throw new NotImplementedException();
    }

    public bool Update(OrderItem obj)
    {
        throw new NotImplementedException();
    }
}

