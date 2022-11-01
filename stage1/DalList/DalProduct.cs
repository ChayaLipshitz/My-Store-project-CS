using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalList;
using Dal.DO;

namespace Dal
{
    internal class DalProduct
    {
        public int create_product(Product product)
        {
            product.Product_ID = DataSource.Config.Product_ID;
            DataSource.ProductsArr[DataSource.Config.Product_ID] = product;
            return product.Product_ID;
            //product.Product_Name = Product_Name;
            //product.Product_Price = Product_Price;
            //product.Product_Category = Product_Category;
            //product.Product_Instock = Product_Instock;
        }
        public Product? read_product(int ID)
        {
           for (int i=0;i< DataSource.Config.product_index; i++)
            {
                if (DataSource.ProductsArr[i].Product_ID == ID)
                {
                    return DataSource.ProductsArr[i];
                }
                
            }
            return null;
            //product.Product_Name = Product_Name;
            //product.Product_Price = Product_Price;
            //product.Product_Category = Product_Category;
            //product.Product_Instock = Product_Instock;
        }
    }
}
