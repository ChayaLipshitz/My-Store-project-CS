
using Dal.DO;
using DalApi;
namespace Dal
{
    public class DalOrder : Iorder
    {

        public  Order[] AllOrders()
        {
            Order[] orders = new Order[DataSource.Config.order_index];
            for (int i = 0; i < DataSource.Config.order_index; i++)
            {
                orders[i] = DataSource.OrdersArr[i];
            }
            return orders;
        }

        public OrderItem[] ProductsInOrder(int ID)
        {
            int sum_items = 0, idx = 0;
            foreach (OrderItem oi in DataSource.OrderItemsArr)
            {
                if (oi.Order_ID == ID)
                {
                    sum_items++;
                }
            }
            OrderItem[] items_in_order = new OrderItem[sum_items];
            foreach (OrderItem oi in DataSource.OrderItemsArr)
            {
                if (oi.Order_ID == ID)
                {
                    items_in_order[idx++] = oi;
                }
            }
            return items_in_order;
        }
        public int Create(Order order)
        {
            if (DataSource.Config.order_index > 49)
            {
                throw new Exception("no place for a new order");
            }
            order.Order_ID = DataSource.Config.Order_ID;
            DataSource.OrdersArr[DataSource.Config.order_index++] = order;
            return order.Order_ID;
        }
        public Order Read(int ID)
        {
            for (int i = 0; i < DataSource.Config.order_index; i++)
            {
                if (DataSource.OrdersArr[i].Order_ID == ID)
                {
                    return DataSource.OrdersArr[i];
                }

            }
            throw new Exception("order id does not exist!");
        }

        public  bool Update(Order order)
        {
            for (int i = 0; i < DataSource.Config.order_index; i++)
            {
                if (DataSource.OrdersArr[i].Order_ID == order.Order_ID)
                {
                    DataSource.OrdersArr[i] = order;
                    return true;
                }
            }
            throw new Exception("the order does not exist!");
        }

        public void Delete(int ID)
        {
            for (int i = 0; i < DataSource.Config.order_index; i++)
            {
                if (DataSource.OrdersArr[i].Order_ID == ID)
                {
                    for (int j = i; j < DataSource.Config.order_index; j++)
                    {
                        DataSource.OrdersArr[j] = DataSource.OrdersArr[j + 1];
                    }
                    DataSource.Config.order_index--;
                    return;
                }
            }
            throw new Exception("the order id does not exist!\n");

        }
    }
}
