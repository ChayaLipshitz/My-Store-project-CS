

using Dal.DO;
using DalApi;

namespace Dal
{
    public class DalProduct: Iproduct
    {
        public int Create(Product product)
        {
            if (DataSource.Config.product_index > 49)
            {
                throw new Exception("no place for a new product");
            }
            product.Product_ID = DataSource.Config.Product_ID;
            DataSource.ProductsArr[DataSource.Config.product_index++] = product;
            return product.Product_ID;
        }
        public Product Read(int ID)
        {
           for (int i=0;i< DataSource.Config.product_index; i++)
            {
                if (DataSource.ProductsArr[i].Product_ID == ID)
                {
                    return DataSource.ProductsArr[i];
                }
                
            }
            throw new Exception("product id does not exist!");
        }
        public bool Update(Product product)
        {
            for (int i = 0; i < DataSource.Config.product_index; i++)
            {
                if (DataSource.ProductsArr[i].Product_ID == product.Product_ID)
                {
                    DataSource.ProductsArr[i] = product;
                    return true;
                }
            }
                throw new Exception("the product does not exist!");
        }
        public void Delete(int ID)
        {
            for (int i = 0; i < DataSource.Config.product_index; i++)
            {
                if (DataSource.ProductsArr[i].Product_ID == ID)
                {
                    for (int j = i; j < DataSource.Config.product_index; j++)
                    {
                        DataSource.ProductsArr[j] = DataSource.ProductsArr[j + 1];
                    }
                    DataSource.Config.product_index--;
                    return;
                }
            }
            throw new Exception("the order id does not exist!\n");
        }
        public Product[] all_products()
        {
            Product[] allProducts = new Product[DataSource.Config.product_index];
            for (int i = 0; i < DataSource.Config.product_index; i++)
            {
                allProducts[i] = DataSource.ProductsArr[i];
            }
            return allProducts;
        }
    }
}

