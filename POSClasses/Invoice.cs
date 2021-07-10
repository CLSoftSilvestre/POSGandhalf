using System;
using System.Collections.Generic;

namespace POSClasses
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public Customer InvoiceCustomer { get; set; }
        public double TotalAmount {get; set;}
        public DateTime InvoiceDate { get; }

        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }

        public void AddLine(InvoiceLine line)
        {
            InvoiceLines.Add(line);
        }

        public void Calculate()
        {
            double temp = 0;

            for (int x=0; x< InvoiceLines.Count; x++)
            {
                InvoiceLines[x].Calculate();
                temp += InvoiceLines[x].Total;
            }
            TotalAmount = temp;
        }

        public void Print()
        {
            Console.WriteLine($"Client: {InvoiceCustomer.Name}");
            Console.WriteLine($"Address: {InvoiceCustomer.Address}");
            Console.WriteLine($"Phone: {InvoiceCustomer.Phone}");
            Console.WriteLine($"VAT: {InvoiceCustomer.VAT}");

            // Imprime as linhas da fatura
            Console.WriteLine("Linhas");
            for (int x=0; x<InvoiceLines.Count; x++)
            {
                Console.WriteLine($"Produto: {InvoiceLines[x].Item.StockProduct.Name}, Qtd: {InvoiceLines[x].Quantity}, Price: {InvoiceLines[x].Total}");
            }
            Console.WriteLine("Total");
            Console.WriteLine(TotalAmount);

        }
    }
}
