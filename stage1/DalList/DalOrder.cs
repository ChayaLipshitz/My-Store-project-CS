
using Dal.DO;
namespace Dal
{
    public class DalOrder
       
    {
        public int create_order(Order order)
        {
            if (DataSource.Config.order_index > 49)
            {
                throw new Exception("no place for a new order");
            }
            order.Order_ID = DataSource.Config.Order_ID;
            DataSource.OrdersArr[DataSource.Config.order_index++] = order;
            return order.Order_ID;
        }
        public Order read_order(int ID)
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
        public Order[] all_orders()
        {
            Order[] orders= new Order[DataSource.Config.order_index];
            for(int i = 0; i < DataSource.Config.order_index; i++)
            {
                orders[i] = DataSource.OrdersArr[i];
            }
            return orders;    
        }
        public void delete_order(int ID)
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
        public void updateOrder(Order order)
        {
            int index=-1;
            for(int i=0; i<DataSource.Config.order_index; i++)
            {
                if(DataSource.OrdersArr[i].Order_ID == order.Order_ID)
                {
                    index = i;
                }
            }
            if (index == -1)
            {
                throw new Exception("the order does not exist!");
            }
            else
            {
                DataSource.OrdersArr[index] = order;
            }
        }
        public OrderItem[] products_in_order(int ID)
        {
            int sum_items=0,idx=0;
          foreach(OrderItem oi in DataSource.OrderItemsArr)
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
        
    }
}
