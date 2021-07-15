using System;
using System.Collections.Generic;

namespace POSClasses
{
    public class Warehouse
    {
        public List<Stock> Products { get; set; } = new List<Stock>();

        public Warehouse()
        {
        }

        public void Add(Stock product)
        {
            Products.Add(product);
        }

        public void Remove(Stock product)
        {
            Products.Remove(product);
        }

        public List<Stock> GetAllProducts()
        {
            return Products;
        }

        public List<Stock> GetProductsWithStock()
        {
            List<Stock> temp = new List<Stock>();

            foreach(Stock item in Products)
            {
                if (item.Quantity > 0)
                {
                    temp.Add(item);
                }
            }
            return temp;
        }

        public List<Stock> GetProductsWithoutStock()
        {
            List<Stock> temp = new List<Stock>();

            foreach (Stock item in Products)
            {
                if (item.Quantity <= 0)
                {
                    temp.Add(item);
                }
            }
            return temp;

        }
    }
}
