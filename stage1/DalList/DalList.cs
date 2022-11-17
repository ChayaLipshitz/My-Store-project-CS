using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal.DO;

namespace Dal
{
    sealed  public  class DalList : IDal

    {
        public Iorder iorder => new DalOrder();

        public IorderItem iorderItem =>  new DalOrderItem();

        public Iproduct iproduct =>  new DalProduct();
    }
}
