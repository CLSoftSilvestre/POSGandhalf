using System;
namespace POSClasses
{
    public class InvoiceLine
    {
        public Stock Item { get; set; }
        public float Quantity { get; set; }
        public float Tax { get; set; } = 18.0f;
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
        }

        public InvoiceLine() { }

        public void Calculate()
        {
            Total = ((Item.StockProduct.Price * Quantity) * Tax) + Item.StockProduct.Price * Quantity;
        }

        public override string ToString()
        {
            return $"Product: {Item.StockProduct.Name}, QTD: {Quantity}, Tax: {Tax}, Total Amount: {Total}";
        }
    }
}
