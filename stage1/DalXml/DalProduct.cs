
using System.Xml.Linq;
using Dal.DO;
using DalApi;
namespace Dal;
internal class DalProduct : Iproduct
{
    /// <summary>
    /// creating a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>return the order id</returns>
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
    /// <summary>
    /// Deleting a certain product
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NotExistExceptions"></exception>
    public void Delete(int id)
    {
        try
        {
            XElement? xdoc = XDocument.Load(@"..\..\xml\Product.xml").Root;
            xdoc.Descendants("Product")
                .Where(x => int.Parse(x.Element("ID").Value) == id).First()
                .Remove();
            xdoc.Save(@"..\..\xml\Product.xml");
        }
        catch(InvalidOperationException ex) {
            throw new NotExistExceptions();
        }
       
    }

    /// <summary>
    /// Reading  the product list by an optional given filter 
    /// </summary>
    /// <returns>the product list</returns>
    public IEnumerable<Product> ReadByFilter(Func<Product, bool> f = null)
    {
        XElement? productsXML = XDocument.Load(@"..\..\xml\Product.xml").Root;
        IEnumerable<Product> allProducts = from p in productsXML?.Elements("Product")
                                           select new Product
                                           {
                                               ID = Convert.ToInt32(p.Element("ID").Value),
                                               Name = p.Element("Name").Value,
                                               Price = Convert.ToDouble(p.Element("Price").Value),
                                               Category = (eCategory)Enum.Parse(typeof(eCategory), p.Element("Category").Value),
                                               InStock = Convert.ToInt32(p.Element("InStock").Value)
                                           };
        if (f == null)      
            return allProducts;        
        return allProducts.Where(f);
    }

    /// <summary>
    /// Reading a certain product by a given condition
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public Product ReadSingle(Func<Product, bool> f)
    {
        XElement? productsXML = XDocument.Load(@"..\..\xml\Product.xml").Root;
        IEnumerable<Product> allProducts = from p in productsXML?.Elements("Product")
                                           select new Product
                                           {
                                               ID = Convert.ToInt32(p.Element("ID").Value),
                                               Name = p.Element("Name").Value,
                                               Price = Convert.ToDouble(p.Element("Price").Value),
                                               Category = (eCategory)Enum.Parse(typeof(eCategory), p.Element("Category").Value),
                                               InStock = Convert.ToInt32(p.Element("InStock").Value)
                                           };
        try
        {
            Product product = allProducts.Where(f).First();
            return product;
        }
        catch (Exception ex)
        {
            throw new NotExistExceptions();
        } 
    }
    /// <summary>
    /// Updating a certain product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>1 in case of succeed</returns>
    /// <exception cref="NotExistExceptions"></exception>
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