using System;
namespace POSClasses
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryDescription { get; set; }
        public float ProductCategoryDefaultTax { get; set; }

        public ProductCategory()
        {
        }
    }
}
