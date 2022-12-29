using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Dal.DO;
using DalApi;
namespace Dal
{
    internal class DalProduct : Iproduct
    {
        public int Create(Product product)
        {
            product.ID = 55555;
            XElement? root = XDocument.Load(@"..\..\xml\Product.xml").Root;
            XElement p = new XElement("Product",
                new XElement("ID", product.ID),
                new XElement("Name", product.Name),
                new XElement("Price", product.Price),
                new XElement("Category", product.Category),
                new XElement("InStock", product.InStock));
            root?.Add(p);
            root?.Save(@"..\..\xml\Product.xml");
            return product.ID;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ReadByFilter(Func<Product, bool> f = null)
        {
            throw new NotImplementedException();
        }

        public Product ReadSingle(Func<Product, bool> f)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
