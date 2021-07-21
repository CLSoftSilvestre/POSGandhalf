using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSClasses
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }

        [NotMapped]
        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

        public virtual IEnumerable<InvoiceLine> Lines { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public double TotalAmount {get; set;}
        public DateTime InvoiceDate { get; set; }
        public bool inProgress { get; set; } = true;
        public int UserId { get; set; }
        public virtual User User { get; set; }

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
            Console.WriteLine($"Client: {Customer.FirstName} {Customer.LastName}");
            Console.WriteLine($"Address: {Customer.Address}");
            Console.WriteLine($"Phone: {Customer.Phone}");
            Console.WriteLine($"VAT: {Customer.VAT}");
            Console.WriteLine($"Invoice date: { InvoiceDate}");

            // Print invoice lines
            Console.WriteLine("Lines");
            for (int x=0; x<InvoiceLines.Count; x++)
            {
                Console.WriteLine($"Product: {InvoiceLines[x].Stock.Product.Name}, Qty: {InvoiceLines[x].Quantity}, Unit: {InvoiceLines[x].Stock.Product.Category.SellingUnit}, Price: {ConvertToMoney(InvoiceLines[x].Stock.Product.Price)}, Tax: {ConvertToPercentage(InvoiceLines[x].Stock.Product.Category.DefaultTax)}, Subtotal: {ConvertToMoney(InvoiceLines[x].Total)}");
            }
            Console.WriteLine("Total amount: " + ConvertToMoney(TotalAmount));

            Console.WriteLine($"Invoiced by {User.FirstName} {User.LastName}");

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
