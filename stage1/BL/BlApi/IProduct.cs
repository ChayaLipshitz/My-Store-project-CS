using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> all_products();
        public IEnumerable<ProductItem> Catalog();
        public IEnumerable<Product> ProductDetails(int id);
        //  public IEnumerable<Product> ProductDetails(int id);
        public void AddProduct(Product product);


    }
}
