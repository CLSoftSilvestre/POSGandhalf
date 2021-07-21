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

            //Remove the quantity from the stock value

            bool success = Item.RemoveFromStock(quantity);
            if (!success)
                Quantity = 0;

            // Calculate the value per line
            Calculate();
        }

        public bool SetQuantity(float qtd)
        {
            if(qtd > 0)
            {
                if (Quantity < qtd)
                {
                    //Verify is we have enougth in stock
                    bool verify = Item.RemoveFromStock(qtd-Quantity);

                    if (verify)
                    {
                        //OK, the stock was updated sucessfully
                        Quantity = qtd;
                        Calculate();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                } else
                {
                    Item.AddToStock(Quantity - qtd);
                    Quantity = qtd;
                    Calculate();
                    return true; 
                }
            }
            return false;
        }

        public void Calculate()
        {
            Total = ((Item.Product.Price * Quantity) * Item.Product.Category.DefaultTax) + Item.Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"Product: {Item.Product.Name}, QTD: {Quantity}, Tax: {Item.Product.Category.DefaultTax}, Total Amount: {Total}";
        }
    }
}
