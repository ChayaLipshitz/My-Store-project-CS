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

        public IEnumerable<Product> all_products();
    }
}
