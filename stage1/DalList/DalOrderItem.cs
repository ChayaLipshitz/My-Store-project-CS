using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DO;
using DalApi;

namespace Dal
{
    internal class DalOrderItem : IorderItem
    {
        public int Create(OrderItem order_item)
        {
            order_item.OrderItem_ID = DataSource.Config.OrderItem_ID;
            DataSource.OrderItemsList.Add(order_item);
            return order_item.OrderItem_ID;
        }
        //public OrderItem Read(int ID)
        //{
        //    foreach(OrderItem oi in DataSource.OrderItemsList)
        //    {
        //        if (oi.OrderItem_ID == ID)
        //        {
        //            return oi;
        //        }
        //    }
        //    throw new NotExistExceptions();
        //}
        public void Delete(int ID)
        {
            foreach (OrderItem order in DataSource.OrderItemsList)
            {
                if (order.OrderItem_ID == ID)
                {
                    DataSource.OrderItemsList.Remove(order);
                    return;
                }
            }
            throw new NotExistExceptions();
        }
        public bool Update(OrderItem order_item)
        {
            for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
            {
                if (DataSource.OrderItemsList[i].OrderItem_ID == order_item.OrderItem_ID)
                {
                    DataSource.OrderItemsList[i] = order_item;
                    return true;
                }
            }
            throw new NotExistExceptions();
        }
        public OrderItem Read_item_by_product_order(int order_id, int product_id)
        {
            foreach (OrderItem oi in DataSource.OrderItemsList)
            {
                if (oi.Order_ID == order_id && oi.Product_ID == product_id)
                {
                    return oi;
                }
            }
            throw new Exception("the order item does not exist!\n");
        }

        private IEnumerable<OrderItem> all_order_items()
        {
            return DataSource.OrderItemsList;
        }
        
        public IEnumerable<OrderItem> ReadByFilter(Func<OrderItem, bool> f = null)
        {

            if (f == null)
            {
                return all_order_items();
            }
            List<OrderItem> OrderItems = new List<OrderItem>();
            foreach (OrderItem orderItem in DataSource.OrderItemsList)
            {
                if (f(orderItem))
                {
                    OrderItems.Add(orderItem);
                }
            }

            return OrderItems;
        }

        public OrderItem ReadSingle(Func<OrderItem, bool> f)
        {
            IEnumerable<OrderItem> OrderItems = all_order_items();
            OrderItem oi;
            return OrderItems.Where(f).First();
            try
            {
                oi = OrderItems.Where(f).First();
                return oi;
            }
            catch (Exception ex)
            {
                throw new NotExistExceptions();
            }
        }
    }
}
