using System;
namespace POSClasses
{
    public class InvoiceLine
    {
        public Stock Item { get; set; }
        public float Quantity { get; set; }
        public double Total { get; set; }

        public InvoiceLine(Stock item, float quantity)
        {
            Item = item;
            Quantity = quantity;

            //Desconta a quantidade do stock:
            float newStock = Item.StockQuantity - Quantity;

            if (newStock >= 0)
            {
                Item.StockQuantity = newStock;
            } else
            {
                Quantity = 0;
            }

            // Calculate the value per line
            Calculate();
        }

        public void Calculate()
        {
            Total = ((Item.StockProduct.Price * Quantity) * Item.StockProduct.Category.ProductCategoryDefaultTax) + Item.StockProduct.Price * Quantity;
        }

        public override string ToString()
        {
            return $"Product: {Item.StockProduct.Name}, QTD: {Quantity}, Tax: {Item.StockProduct.Category.ProductCategoryDefaultTax}, Total Amount: {Total}";
        }
    }
}
