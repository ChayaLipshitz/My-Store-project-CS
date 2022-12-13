
using Dal.DO;
using DalApi;

namespace Dal
{
    internal class DalOrder : Iorder
    {

        private  IEnumerable<Order> AllOrders()
        {
            return DataSource.OrdersList;
        }

        public IEnumerable<OrderItem> ProductsInOrder(int ID)
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
            order.ID = DataSource.Config.Order_ID;
            DataSource.OrdersList.Add(order);
            return order.ID;
        }
        //public Order Read(int ID)
        //{
        //    foreach (Order order in DataSource.OrdersList)
        //    {
        //        if (order.ID == ID)
        //        {
        //            return order;
        //        }
        //    }
        //    throw new NotExistExceptions();
        //}
        public  bool Update(Order new_order)
        {            
            for (int i = 0; i < DataSource.OrdersList.Count(); i++)
            {
                if (DataSource.OrdersList[i].ID == new_order.ID)
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
                if (order.ID == ID)
                {
                    DataSource.OrdersList.Remove(order);
                    return;
                }
            }
            throw new NotExistExceptions();

        }

        public IEnumerable<Order> ReadByFilter(Func<Order, bool> f = null)
        {
           if(f == null)
           {
                return AllOrders();
           }
            IEnumerable<Order> orders = AllOrders();
            return orders.Where(f);

        }

        public Order ReadSingle(Func<Order, bool> f)
        {
            IEnumerable<Order> orders = AllOrders();
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
}
