using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;

namespace BlImplementation;
internal class BlProduct : IProduct
{
    public void AddProduct(BO.Product product)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.ProductForList> all_products()
    {
        throw new NotImplementedException();

    }

    public IEnumerable<BO.ProductItem> Catalog()
    {
        throw new NotImplementedException();

    }

    public IEnumerable<BO.Product> ProductDetails(int id)
    {
        throw new NotImplementedException();

    }
}

