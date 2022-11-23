
using BlApi;
using Dal;
using DalApi;


namespace BlImplementation;
internal class BlProduct : IProduct
{
    IDal DAl = new DalList();

    public void AddProduct(BO.Product product)
    {
    }

    public IEnumerable<BO.ProductForList> all_products()
    {
        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
        IEnumerable<Dal.DO.Product> products = DAl.iproduct.all_products();
       foreach(Dal.DO.Product product in products)
        {
            BO.ProductForList p = new BO.ProductForList();
            p.ID = product.Product_ID;
            p.Name=product.Product_Name;
            p.Price=product.Product_Price;
            p.Category = (BO.eCategory)product.Product_Category;
            productForLists.Add(p);
        }
       return productForLists;
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

