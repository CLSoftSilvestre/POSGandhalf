using System;
using System.Collections.Generic;
using POSClasses;

namespace POSGandhalf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("POSGrandhalf!");
            Console.WriteLine();

            // Criacao da categoria de produto
            ProductCategory MerceariaProdCat = new ProductCategory() { ProductCategoryId = 1, ProductCategoryDescription = "Mercearia", ProductCategoryDefaultTax = 0.18f };

            // Criacao de produto
            Product Banana = new Product(){ProductId=1, Name="Bananas", Description = "Banana da Madeira", Price=12.5f, Category=MerceariaProdCat};
            Product Maracujá = new Product() { ProductId = 2, Name = "Maracujá", Description = "Maracujá dos Açores", Price = 26.99f, Category=MerceariaProdCat};

            // Criacao de stock para produto
            Stock StockBanana = new Stock() { StockProduct = Banana, StockQuantity = 12 };
            Stock StockMaracujá = new Stock() { StockProduct = Maracujá, StockQuantity = 3 };

            // Stock Inicial de produtos na Loja
            Console.WriteLine("---- Stock Inicial ----");
            Console.WriteLine(StockBanana.StockProduct.Name + "  -  " + StockBanana.StockQuantity);
            Console.WriteLine(StockMaracujá.StockProduct.Name + "  -  " + StockMaracujá.StockQuantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();

            // Criação de um Cliente
            Customer Cliente1 = new Customer() { Name = "Celso Silvestre", Address = "Alameda de Belem", CustomerId = 1, Phone = "912152324", VAT = 228885884 };

            // Criação das linhas de fatura
            InvoiceLine LinhaFatura1 = new InvoiceLine(StockBanana, 2);
            InvoiceLine LinhaFatura2 = new InvoiceLine(StockMaracujá, 1);

            // Criacao de uma fatura e adicao das linhas
            Invoice Fatura1 = new Invoice() { InvoiceCustomer = Cliente1 };
            Fatura1.AddLine(LinhaFatura1);
            Fatura1.AddLine(LinhaFatura2);

            // Marcar a fatura como finalizada (caso seja passado true o método imprime a fatura... Neste caso para o console...)
            Console.WriteLine("----- IMPRIMIR FATURA -----");
            Fatura1.Finalize(true);
            Console.WriteLine("----- FIM DA FATURA -----");
            Console.WriteLine();

            //Ver se stock atualizou
            Console.WriteLine("----- Novo Stock -----");
            Console.WriteLine(StockBanana.StockProduct.Name + "  -  " + StockBanana.StockQuantity);
            Console.WriteLine(StockMaracujá.StockProduct.Name + "  -  " + StockMaracujá.StockQuantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }
    }
}
