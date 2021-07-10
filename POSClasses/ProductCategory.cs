using System;
namespace POSClasses
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float DefaultTax { get; set; }
        public Unit SellingUnit { get; set; }

        public ProductCategory()
        {
        }
    }

    public enum Unit
    {
        Unit = 1,
        PCE = 2,
        Pack = 3,
        Kg = 4,
        L = 5,
    }
}
