using BlApi;
using BO;
using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlProduct : IProduct
{
    IDal dal = DalApi.Factory.Get();
    /// <summary>
    /// get a product in BO.Product type and convert it to a Dal.DO.Product type
    /// </summary>
    /// <param name="BOproduct">the product in BO.Product type</param>
    /// <returns>returns the product in Dal.DO.Product type</returns>
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
    /// <summary>
    /// get a product in Dal.DO.Product type and convert it to a get a product in BO.Product type and convert it to a Dal.DO.Product type type
    /// </summary>
    /// <param name="DOproduct">the product in Dal.DO.Product type</param>
    /// <returns>returns the product in BO.Product type</returns>
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

    /// <summary>
    /// check if the product properties are valid
    /// </summary>
    /// <param name="product">product</param>
    /// <exception cref="BO.PropertyInValidException"></exception>
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<BO.ProductForList> ReadAll()
    {
        IEnumerable<Dal.DO.Product> products = dal.iproduct.ReadByFilter();
        IEnumerable<BO.ProductForList> productForList = from product in products
                                                        select new BO.ProductForList
                                                        {
                                                            ID = product.ID,
                                                            Name = product.Name,
                                                            Price = product.Price,
                                                            Category = (BO.eCategory)product.Category
                                                        };
        return productForList;
    }
    /// <summary>
    /// product details for manager screen
    /// </summary>
    /// <param name="ProductId">prudoct id</param>
    /// <returns>product</returns>
    /// <exception cref="BO.NotExistExceptions"></exception>
    /// <exception cref="BO.IDNotValidException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Product ProductDetails(int ProductId)
    {
        BO.Product BLproduct = new BO.Product();
        if (ProductId >= 0)
        {
            try
            {
                Dal.DO.Product DALproduct = dal.iproduct.ReadSingle(p => p.ID == ProductId);
                BLproduct = ConvertToBOproduct(DALproduct);
                return BLproduct;
            }
            catch (Dal.DO.NotExistExceptions err)
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.ProductItem ProductDetails(int ProductId, BO.Cart cart)
    {
        if (ProductId < 0)
            throw new BO.PropertyInValidException("ID");
        try
        {
            Dal.DO.Product DOproduct = dal.iproduct.ReadSingle(p => p.ID == ProductId);
            if (cart.Items.Count() == 0) throw new BO.CartISEmptyException();
            BO.ProductItem BOproductItem = (from oi in cart.Items
                                            where oi.ID == ProductId
                                            select new BO.ProductItem
                                            {
                                                ID = oi.ID,
                                                Name = oi.Name,
                                                Amount = oi.Amount,
                                                Price = oi.Price,
                                                Category = (BO.eCategory)DOproduct.Category,
                                                InStock = DOproduct.InStock > 0
                                            }).First() ?? throw new ProductDoesNoExistInCartExceptions();
            return BOproductItem;
        }
        catch (BO.NotExistExceptions err)
        {
            throw new BO.DataError(err);

        }

    }
    /// <summary>
    /// adding a new product
    /// </summary>
    /// <param name="product">product to add</param>
    /// <exception cref="BO.DataError">error from the database</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

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
    /// <summary>
    /// updating a product
    /// </summary>
    /// <param name="product">updated product</param>
    /// <exception cref="BO.DataError">error from the database</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

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
    /// <summary>
    /// deleting  a product
    /// </summary>
    /// <param name="ProductID">product id to delete</param>
    /// <exception cref="BO.ProductExistsInOrderException"></exception>
    /// <exception cref="BO.DataError">error from the database</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int ProductID)
    {
        IEnumerable<Dal.DO.OrderItem> oi = dal.iorderItem.ReadByFilter(oi => oi.Product_ID == ProductID);
        if (oi.Count() != 0)
            throw new BO.ProductExistsInOrderException();
        try
        {
            lock (dal)
            {
                dal.iproduct.Delete(ProductID);
                return;
            }
        }
        catch (Dal.DO.NotExistExceptions err)
        {
            throw new BO.DataError(err);
        }
    }


    /// <summary>
    /// return all the pruducts which in a specific category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<BO.ProductForList> ReadByCategory(BO.eCategory category)
    {

        IEnumerable<Dal.DO.Product> products = dal.iproduct.ReadByFilter(p => p.Category == (Dal.DO.eCategory)category);
        IEnumerable<BO.ProductForList> productForList = from product in products
                                                        select new BO.ProductForList
                                                        {
                                                            ID = product.ID,
                                                            Name = product.Name,
                                                            Price = product.Price,
                                                            Category = (BO.eCategory)product.Category,
                                                        };
        return productForList;
    }

    /// <summary>
    /// read all the products
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<ProductItem> ReadCatalog()
    {
        IEnumerable<Dal.DO.Product> allProduct = dal.iproduct.ReadByFilter();
        IEnumerable<ProductItem> productItems = from product in allProduct
                                                select new BO.ProductItem
                                                {
                                                    ID = product.ID,
                                                    Name = product.Name,
                                                    Price = product.Price,
                                                    Category = (BO.eCategory)product.Category,
                                                    InStock = product.InStock > 0,
                                                    Amount = product.InStock??0
                                                };
        return productItems;
    }







}

