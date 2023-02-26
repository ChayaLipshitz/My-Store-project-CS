using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalProduct: Iproduct
{
    /// <summary>
    /// creating a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>return the order id</returns>\

    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Create(Product product)
    {
       
        product.ID = DataSource.Config.Product_ID;
        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// Updating a certain product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>1 in case of succeed</returns>
    /// <exception cref="NotExistExceptions"></exception>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]

    public bool Update(Product product)
    {
        int index = DataSource.ProductsList.FindIndex(p => p.ID == product.ID);
        if (index == -1) throw new NotExistExceptions();
        DataSource.ProductsList[index] = product;
        return true;
        
    }

    /// <summary>
    /// Deleting a certain product
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="NotExistExceptions"></exception>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int id)
    {
        try
        {
            Product p = DataSource.ProductsList.Where(p => p.ID == id).First();
            DataSource.ProductsList.Remove(p);
        }
        catch (InvalidOperationException e)
        {
            throw new NotExistExceptions();
        }
    }

    /// <summary>
    /// Reading  the product list by an optional given filter 
    /// </summary>
    /// <returns>the product list</returns>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<Product> ReadByFilter(Func<Product, bool>? f = null)
    {
        if (f == null)
        {
            return DataSource.ProductsList;
        }
        IEnumerable<Product> products = DataSource.ProductsList;
        return products.Where(f);
    }
    /// <summary>
    /// Reading a certain product by a given condition
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    /// <exception cref="NotExistExceptions"></exception>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]

    public Product ReadSingle(Func<Product, bool> f)
    {
        IEnumerable<Product> products = DataSource.ProductsList;
        Product product;
        try
        {
            product = products.Where(f).First();
            return product;
        }
        catch (Exception ex)
        {
           throw new NotExistExceptions();
        }
    }
}

