using System;
namespace POSClasses
{
    public class Stock
    {
        public Product StockProduct { get; set; }
        public float StockQuantity { get; set; }

        public Stock()
        {
        }

        public void UpdateQuantity(float qtd)
        {
            StockQuantity = qtd;
        }
    }
}
