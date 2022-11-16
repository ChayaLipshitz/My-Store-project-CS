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
        public Order[] AllOrders();
        public OrderItem[] ProductsInOrder(int ID);
    }
}
