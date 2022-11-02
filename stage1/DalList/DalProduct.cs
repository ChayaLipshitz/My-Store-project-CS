﻿

using Dal.DO;

namespace Dal
{
    public class DalProduct
    {
        public int create_product(Product product)
        {
            if (DataSource.Config.product_index > 49)
            {
                throw new Exception("no place for a new product");
            }
            product.Product_ID = DataSource.Config.Product_ID;
            DataSource.ProductsArr[DataSource.Config.product_index++] = product;
            return product.Product_ID;
        }
        public Product read_product(int ID)
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
        public Product[] all_products()
        {
            Product[] allProducts=new Product[DataSource.Config.product_index];
            for(int i=0;i< DataSource.Config.product_index;i++)
            {
                allProducts[i]= DataSource.ProductsArr[i];  
            }
            return allProducts;  
        }
        public void delete_product(int ID)
        {
            for(int i = 0; i < DataSource.Config.product_index; i++)
            {
                if(DataSource.ProductsArr[i].Product_ID == ID)
                {
                    for(int j = i; j< DataSource.Config.product_index; j++)
                    {
                        DataSource.ProductsArr[j] = DataSource.ProductsArr[j+1];
                    }
                    DataSource.Config.product_index--;
                    return;
                }
            }
        }
        public void updateProduct(Product product)
        {
            int index = -1;
            for (int i = 0; i < DataSource.Config.product_index; i++)
            {
                if (DataSource.ProductsArr[i].Product_ID == product.Product_ID)
                {
                    index = i;
                }
            }
            if (index == -1)
            {
                throw new Exception("the product does not exist!");
            }
            else
            {
                DataSource.ProductsArr[index] = product;
            }
        }
    }
}

