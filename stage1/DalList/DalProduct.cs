﻿using Dal.DO;
using DalApi;

namespace Dal
{
    internal class DalProduct: Iproduct
    {
        public int Create(Product product)
        {
           
            product.ID = DataSource.Config.Product_ID;
            DataSource.ProductsList.Add(product);
            return product.ID;
        }
        public Product Read(int ID)
        {
            foreach(Product p in DataSource.ProductsList)
            {
                if (p.ID == ID)
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
                if (DataSource.ProductsList[i].ID == product.ID)
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
                if (p.ID == ID)
                {
                    DataSource.ProductsList.Remove(p);
                    return;
                }
            }
            throw new NotExistExceptions();
        }
        private IEnumerable<Product> all_products()
        {            
            return DataSource.ProductsList;
        }

        public IEnumerable<Product> ReadByFilter(Func<Product, bool> f = null)
        {
            if (f == null)
            {
                return all_products();
            }
            List<Product> products = new List<Product>();
            foreach (Product product in DataSource.ProductsList)
            {
                if (f(product))
                {
                    products.Add(product);
                }
            }

            return products;
        }

        
    }
}

