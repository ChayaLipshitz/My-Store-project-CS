using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public interface Iproduct : Icrud<Product>
    {
        public int Create(Product obj);


        public void Delete(int id);


        public Product Read(int id);


        public bool Update(Product obj);
      
    }
}
