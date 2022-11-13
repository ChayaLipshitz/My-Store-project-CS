using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public interface IorderItem : Icrud<OrderItem>
    {
        public int Create(OrderItem obj);


        public void Delete(int id);
       

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
