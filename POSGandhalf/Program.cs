using System;
using System.Collections.Generic;
using POSClasses;

namespace POSGandhalf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Criacao de produto
            Product prod = new Product(){ProductId=1, Name="Produto de teste", Description = "Descricao do produto", Price=12.5f};
            Product prod2 = new Product() { ProductId = 2, Name = "Produto de teste 2", Description = "Descricao do produto 2", Price = 26.99f };
            //Console.WriteLine(prod);

            // Criacao de stock para produto
            Stock stock = new Stock() { StockProduct = prod, StockQuantity = 12 };
            Stock stock2 = new Stock() { StockProduct = prod2, StockQuantity = 3 };

            // Criação de linha de fatura e adicao a lista de linhas
            List<InvoiceLine> InvoiceLines = new List<InvoiceLine>();

            InvoiceLine il = new InvoiceLine(stock, 2);
            il.Calculate();
            //Console.WriteLine(il);
            InvoiceLine il2 = new InvoiceLine(stock2, 1);
            il.Calculate();

            InvoiceLines.Add(il);
            InvoiceLines.Add(il2);

            // Criação de um Customer
            Customer customer = new Customer() { Name = "Celso Silvestre", Address = "Alameda de Belem", CustomerId = 1, Phone = "912152324", VAT = 228885884 };

            //Criacao de uma fatura
            Invoice invoice = new Invoice() { InvoiceCustomer = customer, InvoiceLines = InvoiceLines };
            invoice.Calculate();
            invoice.Print();

            //Ver se stock atualizou
            Console.WriteLine("----------");
            Console.WriteLine(stock.StockProduct.Name + "  -  " + stock.StockQuantity);
            Console.WriteLine(stock2.StockProduct.Name + "  -  " + stock2.StockQuantity);

        }
    }
}
