using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public interface Iorder : Icrud<Order>
    {
        public List<Order> AllOrders();
        public List<OrderItem> ProductsInOrder(int ID);
    }
}
