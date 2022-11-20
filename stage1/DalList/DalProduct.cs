using Dal.DO;
using DalApi;

namespace Dal
{
    internal class DalProduct: Iproduct
    {
        public int Create(Product product)
        {
           
            product.Product_ID = DataSource.Config.Product_ID;
            DataSource.ProductsList.Add(product);
            return product.Product_ID;
        }
        public Product Read(int ID)
        {
            foreach(Product p in DataSource.ProductsList)
            {
                if (p.Product_ID == ID)
                {
                    return p;
                }
            }    
            throw new NotExistExceptions();
        }
        public bool Update(Product product)
        {
            for (int i = 0; i < DataSource.ProductsList.Count(); i++)
            {
                if (DataSource.ProductsList[i].Product_ID == product.Product_ID)
                {
                    DataSource.ProductsList[i] = product;
                    return true;
                }
            }
                throw new NotExistExceptions();
        }
        public void Delete(int ID)
        {
            foreach(Product p in DataSource.ProductsList)
            {
                if (p.Product_ID == ID)
                {
                    DataSource.ProductsList.Remove(p);
                    return;
                }
            }
            throw new NotExistExceptions();
        }
        public IEnumerable<Product> all_products()
        {            
            return DataSource.ProductsList;
        }
    }
}

