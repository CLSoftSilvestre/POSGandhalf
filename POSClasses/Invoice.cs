using System;
using System.Collections.Generic;

namespace POSClasses
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
        public Customer InvoiceCustomer { get; set; }
        public double TotalAmount {get; set;}
        public DateTime InvoiceDate { get; }
        public bool inProgress { get; set; } = true;
        public User InvoicedBy { get; set; }

        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }

        public void AddLine(InvoiceLine line)
        {
            InvoiceLines.Add(line);
            Calculate();
        }

        public void Calculate()
        {
            double temp = 0;

            for (int x=0; x< InvoiceLines.Count; x++)
            {
                //InvoiceLines[x].Calculate();
                temp += InvoiceLines[x].Total;
            }
            TotalAmount = temp;
        }

        public void Finalize(bool print)
        {
            //Set the invoice as finalized and give a number... Optionaly can print.
            inProgress = false;
            if (print)
                Print();
        }

        public void Print()
        {
            Console.WriteLine($"Client: {InvoiceCustomer.FirstName} {InvoiceCustomer.LastName}");
            Console.WriteLine($"Address: {InvoiceCustomer.Address}");
            Console.WriteLine($"Phone: {InvoiceCustomer.Phone}");
            Console.WriteLine($"VAT: {InvoiceCustomer.VAT}");
            Console.WriteLine($"Invoice date: { InvoiceDate}");

            // Imprime as linhas da fatura
            Console.WriteLine("Lines");
            for (int x=0; x<InvoiceLines.Count; x++)
            {
                Console.WriteLine($"Product: {InvoiceLines[x].Item.Article.Name}, Qty: {InvoiceLines[x].Quantity}, Unit: {InvoiceLines[x].Item.Article.Category.SellingUnit}, Price: {ConvertToMoney(InvoiceLines[x].Item.Article.Price)}, Tax: {ConvertToPercentage(InvoiceLines[x].Item.Article.Category.DefaultTax)}, Subtotal: {ConvertToMoney(InvoiceLines[x].Total)}");
            }
            Console.WriteLine("Total amount: " + ConvertToMoney(TotalAmount));

            Console.WriteLine($"Invoiced by {InvoicedBy.FirstName} {InvoicedBy.LastName}");

        }

        public static string ConvertToMoney(double Value)
        {
            return string.Format("{0:#.00€}", Convert.ToDecimal(Value));
        }

        public static string ConvertToPercentage(float Value)
        {
            return string.Format("{0:#.0%}", Convert.ToDecimal(Value));
        }
    }
}
