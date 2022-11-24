
using BlApi;
using Dal;
using DalApi;


namespace BlImplementation;
internal class BlProduct : IProduct
{
    IDal DAl = new DalList();
    public IEnumerable<BO.ProductForList> ReadAll()
    {
        List<BO.ProductForList> productForList = new List<BO.ProductForList>();
        IEnumerable<Dal.DO.Product> products = DAl.iproduct.all_products();
       foreach(Dal.DO.Product product in products)
        {
            BO.ProductForList p = new BO.ProductForList();
            p.ID = product.ID;
            p.Name=product.Name;
            p.Price=product.Price;
            p.Category = (BO.eCategory)product.Category;
            productForList.Add(p);
        }
       return productForList;
    }
    /// <summary>
    /// product details for manager screen
    /// </summary>
    /// <param name="ProductId">prudoct id</param>
    /// <returns>product</returns>
    /// <exception cref="BO.NotExistExceptions"></exception>
    /// <exception cref="BO.IDNotValidException"></exception>
    public BO.Product ProductDetails(int ProductId)
    {
        BO.Product BLproduct = new BO.Product();
        if (ProductId >= 0)
        {
            try
            {
                Dal.DO.Product DALproduct = DAl.iproduct.Read(ProductId);
                BLproduct.ID = DALproduct.ID;
                BLproduct.Name = DALproduct.Name;
                BLproduct.Price = DALproduct.Price;
                BLproduct.Category = (BO.eCategory)DALproduct.Category;
                BLproduct.InStock = DALproduct.Instock;
            }
            catch (BO.NotExistExceptions ex)
            {
                throw new BO.NotExistExceptions();  ////--------------------------------///

            }
        }
        else
        {
            throw new BO.IDNotValidException();
        }
        return BLproduct;

    }
    /// <summary>
    ///  product details for client screen
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem ProductDetails(int ProductId, BO.Cart cart)
    {

        BO.ProductItem BLproductItem = new BO.ProductItem();
        if (ProductId >= 0)
        {
            try
            {
                int amount =cart.Items.Find(product => product.ID == ProductId).Amount;
                Dal.DO.Product DALproduct = DAl.iproduct.Read(ProductId);
                BLproductItem.ID = DALproduct.ID;
                BLproductItem.Name = DALproduct.Name;
                BLproductItem.Price = DALproduct.Price;
                BLproductItem.Category = (BO.eCategory)DALproduct.Category;
                BLproductItem.InStock = DALproduct.Instock>0;
                BLproductItem.Amount = amount;
            }
            catch (BO.NotExistExceptions ex)
            {
                throw new BO.NotExistExceptions();  ////--------------------------------///

            }
            catch(Exception ex)
            {
                throw new BO.NotExistExceptions();
            }
        }
        else
        {
            throw new BO.IDNotValidException();
        }
        return BLproductItem;
    }

    public void Add(BO.Product product)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    

   
   
}

