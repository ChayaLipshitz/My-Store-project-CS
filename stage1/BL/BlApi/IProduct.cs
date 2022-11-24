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
        public IEnumerable<ProductForList> ReadAll();
        public BO.Product ProductDetails(int ProductId);
        public BO.ProductItem ProductDetails(int ProductId, Cart cart);
        public void Add(BO.Product product);
        public void Update(BO.Product product);
        public void Delete(int id);
    }
}
