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
        public void Delete(int ID)
        {
            try
            {
                OrderItem oi = DataSource.OrderItemsList.Where(oi => oi.OrderItem_ID == ID).First();
                DataSource.OrderItemsList.Remove(oi);
            }
            catch (InvalidOperationException e)
            {
                throw new NotExistExceptions();
            }
        }
        public bool Update(OrderItem order_item)
        {
            int index = DataSource.OrderItemsList.FindIndex(oi => order_item.OrderItem_ID == oi.OrderItem_ID);
            if (index == -1) throw new NotExistExceptions();
            DataSource.OrderItemsList[index] = order_item;
            return true;
        }
        public OrderItem Read_item_by_product_order(int order_id, int product_id)
        {
            try
            {
                return DataSource.OrderItemsList.Where((oi) => oi.Order_ID == order_id && oi.Product_ID == product_id).First();
                
            }
            catch (InvalidOperationException e)
            {
                throw new NotExistExceptions();
            }
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
            IEnumerable<OrderItem> OrderItems = all_order_items();
            return OrderItems.Where(f);
        }

        public OrderItem ReadSingle(Func<OrderItem, bool> f)
        {

            try
            {
                OrderItem oi = all_order_items().Where(f).First();
                return oi;
            }
            catch (Exception ex)
            {
                throw new NotExistExceptions();
            }
        }
    }
}
