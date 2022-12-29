using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalOrder : Iorder
    {
        public int Create(Order obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> ProductsInOrder(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadByFilter(Func<Order, bool> f = null)
        {
            throw new NotImplementedException();
        }

        public Order ReadSingle(Func<Order, bool> f)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
