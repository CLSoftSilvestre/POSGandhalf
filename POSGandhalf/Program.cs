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
            User utilizador1 = new User() {
                UserLoginName= "csilvestre",
                UserPassword = "1234",
                FirstName="Celso", LastName="Silvestre",
                UserRole=Role.Operator
            };

            // Fazer login (Simulado uma vez que ainda não tenho EntityFramework...
            User.Login("csilvestre", "1234");

            // Criacao da categoria de produto
            ProductCategory Frutas = new ProductCategory() {
                Id = 1,
                Description = "Frutaria",
                DefaultTax = 0.16f,
                SellingUnit = Unit.Kg
            };

            // Criacao de produto
            Product Banana = new Product(){
                ProductId=1,
                Name="Bananas",
                Description = "Banana da Madeira",
                Price=12.5f,
                Category=Frutas
            };

            Product Maracujá = new Product() {
                ProductId = 2,
                Name = "Maracujá",
                Description = "Maracujá dos Açores",
                Price = 26.99f,
                Category=Frutas
            };

            // Criacao de stock para produto
            Stock StockBanana = new Stock() { Article = Banana, Quantity = 12 };
            Stock StockMaracujá = new Stock() { Article = Maracujá, Quantity = 3 };

            // Adicionar os produtods criados a um armazem
            Warehouse Loja1 = new Warehouse();
            Loja1.Add(StockBanana);
            Loja1.Add(StockMaracujá);

            // Stock Inicial de produtos na Loja
            Console.WriteLine("---- Stock Inicial ----");
            Console.WriteLine(Loja1.Products[0].Article.Name + "  -  " + Loja1.Products[0].Quantity);
            Console.WriteLine(Loja1.Products[1].Article.Name + "  -  " + Loja1.Products[1].Quantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();

            // Criação de um Cliente
            Customer Cliente1 = new Customer() {
                Id = 1,
                FirstName = "João",
                LastName="Almeida",
                Address = "Avenida 6 de Janeiro",
                Phone = "913857132",
                VAT = 203453356
            };

            // Apenas efectuar a faturação caso o ulizador tenha o login efectuado

            if (User.UserHasLoginActive())
            {
                // Criação das linhas de fatura
                InvoiceLine LinhaFatura1 = new InvoiceLine(Loja1.Products[0], 2);
                InvoiceLine LinhaFatura2 = new InvoiceLine(Loja1.Products[1], 1);

                // Criacao de uma fatura e adicao das linhas
                Invoice Fatura1 = new Invoice() { InvoiceCustomer = Cliente1, InvoicedBy=utilizador1};
                Fatura1.AddLine(LinhaFatura1);
                Fatura1.AddLine(LinhaFatura2);

                // Atualizar quantidade de produtos da linha 1
                LinhaFatura1.SetQuantity(5);

                // Marcar a fatura como finalizada (caso seja passado true o método imprime a fatura...)
                Console.WriteLine("----- IMPRIMIR FATURA -----");
                Fatura1.Finalize(true);
                Console.WriteLine("----- FIM DA FATURA -----");
                Console.WriteLine();

            }

            // Ver se stock atualizou
            Console.WriteLine("----- Novo Stock -----");
            Console.WriteLine(Loja1.Products[0].Article.Name + "  -  " + Loja1.Products[0].Quantity);
            Console.WriteLine(Loja1.Products[1].Article.Name + "  -  " + Loja1.Products[0].Quantity);
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }
    }
}
