using System;
namespace POSClasses
{
    public class InvoiceLine
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }

        public float Quantity { get; set; }
        public double Total { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public InvoiceLine()
        {

        }

        public InvoiceLine(Stock item, float quantity)
        {
            Stock = item;
            Quantity = quantity;

            //Remove the quantity from the stock value

            bool success = Stock.RemoveFromStock(quantity);
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
                    bool verify = Stock.RemoveFromStock(qtd-Quantity);

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
                    Stock.AddToStock(Quantity - qtd);
                    Quantity = qtd;
                    Calculate();
                    return true; 
                }
            }
            return false;
        }

        public void Calculate()
        {
            Total = ((Stock.Product.Price * Quantity) * Stock.Product.Category.DefaultTax) + Stock.Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"Product: {Stock.Product.Name}, QTD: {Quantity}, Tax: {Stock.Product.Category.DefaultTax}, Total Amount: {Total}";
        }
    }
}
