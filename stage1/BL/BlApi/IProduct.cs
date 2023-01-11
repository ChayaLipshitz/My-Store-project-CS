using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList> ReadAll();
        public IEnumerable<BO.ProductForList> ReadByCategory(BO.eCategory category);
        public BO.Product ProductDetails(int ProductId);
        public BO.ProductItem ProductDetails(int ProductId, BO.Cart cart);
        public void Add(BO.Product product);
        public void Update(BO.Product product);
        public void Delete(int id);
    }
}
