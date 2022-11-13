using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public class IorderItem : Icrud<OrderItem>
    {
        public int Create(OrderItem obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderItem Read(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(OrderItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
