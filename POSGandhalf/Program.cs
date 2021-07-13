using System;
using System.Collections.Generic;
using POSClasses;
using Widgets;

namespace POSGandhalf
{
    class Program
    {
        static Screen Screen1 = new Screen();
        static readonly ConsoleColor PRIMARY_COLOR = ConsoleColor.White;
        static readonly ConsoleColor SECONDARY_COLOR = ConsoleColor.DarkBlue;

        static void Main(string[] args)
        {
            SetupScreen();

            //Codigo para ignorar
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

        static void SetupScreen()
        {
            // Adds one background to the screen app
            Background bg = new Background(0, 0, 120, 30, SECONDARY_COLOR, BackgroundType.Light);
            Screen1.Add(bg);

            // Adds one main menu to the app and sets the events
            List<MenuItem> AdminMenuItems = new();

            MenuItem LoginMnuItem = new MenuItem("Login");
            LoginMnuItem.Selected += LoginMnuItem_Selected;
            AdminMenuItems.Add(LoginMnuItem);

            MenuItem LogoutMnuItem = new MenuItem("Logout");
            LogoutMnuItem.Selected += LogoutMnuItem_Selected;  
            AdminMenuItems.Add(LogoutMnuItem);

            MenuItem NewUserMnuItem = new MenuItem("Criar novo utilizador");
            NewUserMnuItem.Selected += NewUserMnuItem_Selected;
            AdminMenuItems.Add(NewUserMnuItem);

            MenuItem ExitAppMnuItem = new MenuItem("Sair da aplicação");
            ExitAppMnuItem.Selected += ExitAppMnuItem_Selected;
            AdminMenuItems.Add(ExitAppMnuItem);

            // Created the Menu for administration
            Menu LoginMnu = new Menu("Menu de administração", AdminMenuItems);
            LoginMnu.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Set the login menu to the Screen and wait for the selection of the user
            Screen1.Add(LoginMnu);
            Screen1.Render();
            LoginMnu.Select();
        }

        private static void ExitAppMnuItem_Selected(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private static void NewUserMnuItem_Selected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void LogoutMnuItem_Selected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void LoginMnuItem_Selected(object sender, EventArgs e)
        {
            // Draw Login Form
            Form frmLogin = new(100, 15, "Login", Screen1);
            frmLogin.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            Screen1.Add(frmLogin);
            Screen1.Render();
            Console.ReadKey();
            frmLogin.Dispose(Screen1);
        }

    }
}
