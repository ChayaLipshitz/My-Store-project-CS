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


    /////////// category
    ///...... delete
{
    internal class DalProduct : Iproduct
    {
        public int Create(Product product)
        {
            XElement? IDS = XDocument.Load("../../xml/ConfigData.xml").Root;
            int ProductID = Convert.ToInt32(IDS?.Element("ProductId")?.Value);
            product.ID = ProductID;
            ProductID++;
            IDS.Element("ProductId").Value = ProductID.ToString();
            IDS.Save("../../xml/ConfigData.xml");
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
            XElement? productsXML = XDocument.Load(@"..\..\xml\Product.xml").Root;
            IEnumerable<Product> allProducts = from p in productsXML?.Elements("Product")
                                               select new Product 
                                               {
                                                   ID = Convert.ToInt32(p.Element("ID").Value),
                                                   Name = p.Element("Name").Value,
                                                   Price = Convert.ToInt32(p.Element("Price").Value),
                                                   Category = eCategory.FROZEN,///////////////////////-----/////////////
                                                   InStock = Convert.ToInt32(p.Element("InStock").Value)
                                               };
            if (f == null)
            {
                return allProducts;
            }
            return allProducts.Where(f);
        }


        public Product ReadSingle(Func<Product, bool> f)
        {
            XElement? productsXML = XDocument.Load(@"..\..\xml\Product.xml").Root;
            IEnumerable<Product> allProducts = from p in productsXML?.Elements("Product")
                                               select new Product
                                               {
                                                   ID = Convert.ToInt32(p.Element("ID").Value),
                                                   Name = p.Element("Name").Value,
                                                   Price = Convert.ToInt32(p.Element("Price").Value),
                                                   Category = eCategory.FROZEN,///////////////////////-----/////////////
                                                   InStock = Convert.ToInt32(p.Element("InStock").Value)
                                               };
            return allProducts.Where(f).First();
        }

        public bool Update(Product updatedPro)
        {
            try
            {
                XElement? products = XDocument.Load(@"..\..\xml\Product.xml").Root;
                XElement product = products?.Elements("Product").Where(p => Convert.ToInt32(p.Element("ID").Value) == updatedPro.ID).FirstOrDefault() ?? throw new NotExistExceptions();
                product.Element("Name").Value = updatedPro.Name;
                product.Element("Price").Value = updatedPro.Price.ToString();
                product.Element("Category").Value = updatedPro.Category.ToString();
                product.Element("InStock").Value = updatedPro.InStock.ToString();
                products?.Save(@"..\..\xml\Product.xml");
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }

}