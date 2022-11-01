﻿
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
       // public Product[] products_in_order(int ID)
       // {
            
        //}
    }
}
