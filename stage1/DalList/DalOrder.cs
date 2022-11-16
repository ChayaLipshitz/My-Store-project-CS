
using Dal.DO;
using DalApi;
namespace Dal
{
    public class DalOrder : Iorder
    {

        public  List<Order> AllOrders()
        {
            return DataSource.OrdersList;
        }

        public List<OrderItem> ProductsInOrder(int ID)
        {
            List<OrderItem> orderItem = new List<OrderItem>();
            foreach (OrderItem oi in DataSource.OrderItemsList)
            {
                if (oi.Order_ID == ID)
                {
                    orderItem.Add(oi);
                }
            }
            return orderItem;
        }
        public int Create(Order order)
        {            
            order.Order_ID = DataSource.Config.Order_ID;
            DataSource.OrdersList.Add(order);
            return order.Order_ID;
        }
        public Order Read(int ID)
        {
            foreach (Order order in DataSource.OrdersList)
            {
                if (order.Order_ID == ID)
                {
                    return order;
                }
            }
            throw new NotExistExceptions();
        }

        public  bool Update(Order new_order)
        {            
            for (int i = 0; i < DataSource.OrdersList.Count(); i++)
            {
                if (DataSource.OrdersList[i].Order_ID == new_order.Order_ID)
                {
                    DataSource.OrdersList[i] = new_order;
                    return true;
                }
            }
            throw new NotExistExceptions();
        }

        public void Delete(int ID)
        {
            foreach(Order order in DataSource.OrdersList)
            {
                if (order.Order_ID == ID)
                {
                    DataSource.OrdersList.Remove(order);
                    return;
                }
            }
            throw new NotExistExceptions();

        }
    }
}
