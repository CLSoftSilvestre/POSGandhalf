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

            // Criacao de um utilizador de POS
            User utilizador1 = new User() { UserLoginName= "csilvestre", UserPassword = "1234", FirstName="Celso", LastName="Silvestre" };

            // Fazer login (Simulado uma vez que ainda não tenho EntityFramework...
            User.Login("csilvestre", "1234");

            // Criacao da categoria de produto
            ProductCategory MerceariaProdCat = new ProductCategory() { Id = 1, Description = "Mercearia", DefaultTax = 0.18f, SellingUnit = Unit.Kg };

            // Criacao de produto
            Product Banana = new Product(){ProductId=1, Name="Bananas", Description = "Banana da Madeira", Price=12.5f, Category=MerceariaProdCat};
            Product Maracujá = new Product() { ProductId = 2, Name = "Maracujá", Description = "Maracujá dos Açores", Price = 26.99f, Category=MerceariaProdCat};

            // Criacao de stock para produto
            Stock StockBanana = new Stock() { Article = Banana, Quantity = 12 };
            Stock StockMaracujá = new Stock() { Article = Maracujá, Quantity = 3 };

            // Stock Inicial de produtos na Loja
            Console.WriteLine("---- Stock Inicial ----");
            Console.WriteLine(StockBanana.Article.Name + "  -  " + StockBanana.Quantity);
            Console.WriteLine(StockMaracujá.Article.Name + "  -  " + StockMaracujá.Quantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();

            // Criação de um Cliente
            Customer Cliente1 = new Customer() { FirstName = "Celso", LastName="Silvestre", Address = "Alameda de Belem", Id = 1, Phone = "912152324", VAT = 228885884 };

            // Apenas efectuar a faturação caso o tulizador tenha o login efectuado

            if (User.UserHasLoginActive())
            {
                // Criação das linhas de fatura
                InvoiceLine LinhaFatura1 = new InvoiceLine(StockBanana, 2);
                InvoiceLine LinhaFatura2 = new InvoiceLine(StockMaracujá, 1);

                // Criacao de uma fatura e adicao das linhas
                Invoice Fatura1 = new Invoice() { InvoiceCustomer = Cliente1, InvoicedBy=utilizador1};
                Fatura1.AddLine(LinhaFatura1);
                Fatura1.AddLine(LinhaFatura2);

                // Atualizar quantidade de produtos da linha 1
                LinhaFatura1.SetQuantity(5);

                // Marcar a fatura como finalizada (caso seja passado true o método imprime a fatura... Neste caso para o console...)
                Console.WriteLine("----- IMPRIMIR FATURA -----");
                Fatura1.Finalize(true);
                Console.WriteLine("----- FIM DA FATURA -----");
                Console.WriteLine();

            }

            // Ver se stock atualizou
            Console.WriteLine("----- Novo Stock -----");
            Console.WriteLine(StockBanana.Article.Name + "  -  " + StockBanana.Quantity);
            Console.WriteLine(StockMaracujá.Article.Name + "  -  " + StockMaracujá.Quantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }
    }
}
