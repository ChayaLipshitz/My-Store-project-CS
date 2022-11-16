using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DO;
using DalApi;

namespace Dal
{
    public class DalOrderItem:IorderItem
    {
        public int Create(OrderItem order_item)
        {
            if (DataSource.Config.orderItem_index > 49)
            {
                throw new Exception("no place for a new order item");
            }
            order_item.OrderItem_ID = DataSource.Config.OrderItem_ID;
            DataSource.OrderItemsArr[DataSource.Config.orderItem_index++] = order_item;
            return order_item.OrderItem_ID;
        }
        public OrderItem Read(int ID)
        {
            for (int i = 0; i < DataSource.Config.orderItem_index; i++)
            {
                if (DataSource.OrderItemsArr[i].OrderItem_ID == ID)
                {
                    return DataSource.OrderItemsArr[i];
                }

            }
            throw new Exception("order_item id does not exist!");

        }
        public void Delete(int ID)
        {
            for (int i = 0; i < DataSource.Config.orderItem_index; i++)
            {
                if (DataSource.OrderItemsArr[i].OrderItem_ID == ID)
                {
                    for (int j = i; j < DataSource.Config.orderItem_index; j++)
                    {
                        DataSource.OrderItemsArr[j] = DataSource.OrderItemsArr[j + 1];
                    }
                    DataSource.Config.orderItem_index--;
                    return;
                }
            }
            throw new Exception("the order id does not exist!\n");

        }
        public bool Update(OrderItem order_item)
        {
            for (int i = 0; i < DataSource.Config.orderItem_index; i++)
            {
                if (DataSource.OrderItemsArr[i].OrderItem_ID == order_item.OrderItem_ID)
                {
                    DataSource.OrderItemsArr[i] = order_item;
                    return true;
                }
            }
                throw new Exception("the order_item does not exist!");
            
        }
        public OrderItem Read_item_by_product_order(int order_id, int product_id)
        {
            foreach (OrderItem oi in DataSource.OrderItemsArr)
            {
                if (oi.Order_ID == order_id && oi.Product_ID == product_id)
                {
                    return oi;
                }
            }
            throw new Exception("the order item does not exist!\n");
        }

        public OrderItem[] all_order_items()
        {
            OrderItem[] orderItems = new OrderItem[DataSource.Config.orderItem_index];
            for (int i = 0; i < DataSource.Config.orderItem_index; i++)
            {
                orderItems[i] = DataSource.OrderItemsArr[i];
            }
            return orderItems;
        }
    }
}
