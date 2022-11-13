using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public interface Iorder: Icrud<Order>
    {
        int Create(Order obj);
        Order Read(int id);
        bool Update(Order obj);
        void Delete(int id);
    }
}
