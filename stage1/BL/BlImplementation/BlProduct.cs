
using BlApi;
using Dal;
using DalApi;


namespace BlImplementation;
internal class BlProduct : IProduct
{
    IDal DAl = new DalList();
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
    private bool CheckObjValidation(BO.Product product)
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
        return true;
    }
    /// <summary>
    /// all products as ProductForList
    /// </summary>
    /// <returns>IEnumerable of all the products as ProductForList</returns>
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
                BLproduct = ConvertToBOproduct(DALproduct);
            }
            catch (BO.NotExistExceptions err)
            {
                throw new BO.DataError(err);  ////--------------------------------///
            }
            return BLproduct;
        }
            throw new BO.IDNotValidException();
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
                BLproductItem.InStock = DALproduct.InStock>0;
                BLproductItem.Amount = amount;
            }
            catch (BO.NotExistExceptions err)
            {
                throw new BO.DataError(err);  ////--------------------------------///

            }
            catch(Exception err)
            {
                throw new BO.DataError(err);
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
        if (CheckObjValidation(product))
        {
            try
            {
                DAl.iproduct.Create(ConvertToDOproduct(product));
            }
            catch (Dal.DO.DuplicateIdExceptions err)
            {
                throw new BO.DataError(err);
            }
        }
    }

    public void Update(BO.Product product)
    {
        if (CheckObjValidation(product))
        {
            try
            {
                DAl.iproduct.Update(ConvertToDOproduct(product));
            }
            catch (Dal.DO.NotExistExceptions err)
            {
                throw new BO.DataError(err);
            }
        }

    } 
    public void Delete(int id)
    {
        bool flag = true;
        foreach( Dal.DO.Order order in DAl.iorder.AllOrders())
        {
            if (order.ID == id)
            {
                flag = false;
            }
        }
        if (!flag)
        {
            throw new BO.ProductExistsInOrderException();
        }
        try
        {
                DAl.iorder.Delete(id);
        }
        catch(Dal.DO.NotExistExceptions err)
        {
                throw new BO.DataError(err);
        }
       
       
    }

    

   
   
}

