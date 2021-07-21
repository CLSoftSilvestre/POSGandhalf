using System;
using System.Collections.Generic;
using POSClasses;

namespace POSClasses
{
    
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {Name}, Description: {Description}, Price: {Price}";
        }

    }
}
