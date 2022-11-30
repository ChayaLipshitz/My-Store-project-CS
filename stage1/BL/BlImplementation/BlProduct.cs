
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;
internal class BlProduct : IProduct
{
    IDal dal = new DalList();
    private Dal.DO.Product ConvertToDOproduct(BO.Product BOproduct)
    {
        Dal.DO.Product DOproduct = new Dal.DO.Product();
        DOproduct.ID = BOproduct.ID;
        DOproduct.Name = BOproduct.Name;
        DOproduct.Price = BOproduct.Price;
        DOproduct.Category = (Dal.DO.eCategory)BOproduct.Category;
        DOproduct.InStock = BOproduct.InStock;
        return DOproduct;
    }
    private BO.Product ConvertToBOproduct(Dal.DO.Product DOproduct)
    {
        BO.Product BOproduct = new BO.Product();
        BOproduct.ID = DOproduct.ID;
        BOproduct.Name = DOproduct.Name;
        BOproduct.Price = DOproduct.Price;
        BOproduct.Category = (BO.eCategory)DOproduct.Category;
        BOproduct.InStock = DOproduct.InStock;
        return BOproduct;
    }
    private void CheckObjValidation(BO.Product product)
    {
        if (product.ID < 0)
        {
            throw new BO.PropertyInValidException("Id");
        }
        if (product.Name == "")
        {
            throw new BO.PropertyInValidException("name");
        }
        if (product.Price < 0)
        {
            throw new BO.PropertyInValidException("price");
        }
        if (product.InStock < 0)
        {
            throw new BO.PropertyInValidException("quantity in stock");
        }
    }
    /// <summary>
    /// all products as ProductForList
    /// </summary>
    /// <returns>IEnumerable of all the products as ProductForList</returns>
    public IEnumerable<BO.ProductForList> ReadAll()
    {
        List<BO.ProductForList> productForList = new List<BO.ProductForList>();
        IEnumerable<Dal.DO.Product> products = dal.iproduct.all_products();
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
                Dal.DO.Product DALproduct = dal.iproduct.Read(ProductId);
                BLproduct = ConvertToBOproduct(DALproduct);
                return BLproduct;
            }
            catch (BO.NotExistExceptions err)
            {
                throw new BO.DataError(err); ////--------------------------------///
            }
        }
            throw new BO.PropertyInValidException("ID");
    }
    /// <summary>
    ///  product details for client screen
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem ProductDetails(int ProductId, BO.Cart cart)
    {
        if (ProductId < 0)
            throw new BO.PropertyInValidException("ID");
        BO.ProductItem BOproductItem = new BO.ProductItem();
        try
        {
            Dal.DO.Product DOproduct = dal.iproduct.Read(ProductId);
            BOproductItem.ID = DOproduct.ID;
            BOproductItem.Name = DOproduct.Name;
            BOproductItem.Price = DOproduct.Price;
            BOproductItem.Category = (BO.eCategory)DOproduct.Category;
            BOproductItem.InStock = DOproduct.InStock>0;
            BOproductItem.Amount = cart.Items.Find(oi => oi.ProductID == ProductId).Amount;
            
            return BOproductItem;
        }
        catch (BO.NotExistExceptions err)
        {
            throw new BO.DataError(err);  

        }
        catch(Exception err)
        {
            throw new BO.DataError(err);
        }       
    }
    
    public void Add(BO.Product product)
    {
        try
        {
            CheckObjValidation(product);
            dal.iproduct.Create(ConvertToDOproduct(product));
        }
        catch (Dal.DO.DuplicateIdExceptions err)
        {
            throw new BO.DataError(err);
        }
        catch (BO.PropertyInValidException ex)
        {
            throw ex;
        }
    }

    public void Update(BO.Product product)
    {
        try
        {
            CheckObjValidation(product);
            dal.iproduct.Update(ConvertToDOproduct(product));
        }
        catch (Dal.DO.NotExistExceptions err)
        {
            throw new BO.DataError(err);
        }
        catch (BO.PropertyInValidException ex)
        {
            throw ex;
        }
    } 
    public void Delete(int ProductID)
    {
        // or to check just in al the ordderItems in dal  ?????
        foreach( Dal.DO.OrderItem orderItem in dal.iorderItem.all_order_items())
        {
                if (orderItem.Product_ID == ProductID)
                    throw new BO.ProductExistsInOrderException();
        }
        try
        {
            dal.iorder.Delete(ProductID);
        }
        catch(Dal.DO.NotExistExceptions err)
        {
            throw new BO.DataError(err);
        }
    }
}

