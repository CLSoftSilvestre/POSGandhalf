using System;
using System.Collections.Generic;
using System.Linq;
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

        }

        static void SetupScreen()
        {
            // Adds one background to the screen app
            Background bg = new Background(0, 0, 120, 30, SECONDARY_COLOR, BackgroundType.Light);
            Screen1.Add(bg);
            AdministrationMenu();
        }

        static void AdministrationMenu()
        {
            // Adds one main menu to the app and sets the events
            List<MenuItem> AdminMenuItems = new();

            MenuItem LoginMnuItem = new MenuItem("Login");
            LoginMnuItem.Selected += LoginMnuItem_Selected;
            AdminMenuItems.Add(LoginMnuItem);

            MenuItem ExitAppMnuItem = new MenuItem("Exit application");
            ExitAppMnuItem.Selected += ExitAppMnuItem_Selected;
            AdminMenuItems.Add(ExitAppMnuItem);

            // Created the Menu for administration
            Menu LoginMnu = new Menu("Administration Menu", AdminMenuItems);
            LoginMnu.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Set the login menu to the Screen and wait for the selection of the user
            Screen1.Add(LoginMnu);
            Screen1.Render();
            LoginMnu.Select();

        }

        static void InvoiceMenu()
        {
            // Adds one main menu to the app and sets the events
            List<MenuItem> InvoiceMenuItems = new();

            MenuItem NewInvoiceMnuItem = new MenuItem("New Invoice");
            //LoginMnuItem.Selected += LoginMnuItem_Selected;
            InvoiceMenuItems.Add(NewInvoiceMnuItem);

            MenuItem ViewStockMnuItem = new MenuItem("View Stock");
            ViewStockMnuItem.Selected += ViewStockMnuItem_Selected;
            InvoiceMenuItems.Add(ViewStockMnuItem);

            MenuItem BackMnuItem = new MenuItem("Back");
            BackMnuItem.Selected += BackMnuItem_Selected;
            InvoiceMenuItems.Add(BackMnuItem);

            // Created the Menu for administration
            Menu LoginMnu = new Menu("Invoice Menu", InvoiceMenuItems);
            LoginMnu.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Set the login menu to the Screen and wait for the selection of the user
            Screen1.Add(LoginMnu);
            Screen1.Render();
            LoginMnu.Select();

        }

        private static void BackMnuItem_Selected(object sender, EventArgs e)
        {
            //Do noting and get back
            AdministrationMenu();
        }

        private static void ExitAppMnuItem_Selected(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private static void LoginMnuItem_Selected(object sender, EventArgs e)
        {
            // Draw Login Form
            Form frmLogin = new(50, 15, "Login", Screen1);
            frmLogin.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Draw TextBoxes for UserName and Password
            TextBox txtUsername = new TextBox(50, 11, 20, "Username");
            txtUsername.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            frmLogin.Add(txtUsername);

            TextBox txtPassword = new TextBox(50, 16, 20, "Password");
            txtPassword.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            frmLogin.Add(txtPassword);

            // Add form to screen and render the screen 
            Screen1.Add(frmLogin);
            Screen1.Render();

            // Go to controls to input the values
            string Username = txtUsername.Select();
            string Password = txtPassword.Select();

            // Clear form form screen
            frmLogin.Dispose(Screen1);

            // Login using database
            using(DataContext context = new())
            {
                var user = context.Users.Where(usr => usr.UserLoginName == Username).FirstOrDefault();

                if (user == null)
                {
                    MsgBox msgNoUser = new MsgBox("Login error", "User not found...");
                    msgNoUser.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                    msgNoUser.Show(Screen1);
                } else
                {
                    if(user.UserPassword != Password)
                    {
                        MsgBox msgWrongPass = new MsgBox("Login error", "Wrong password...");
                        msgWrongPass.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                        msgWrongPass.Show(Screen1);
                    } else
                    {
                        // User autenticated
                        User.CurrentUser = user;
                        InvoiceMenu();
                    }
                }
            }

        }

        private static void ViewStockMnuItem_Selected(object sender, EventArgs e)
        {
            // View products list
            Form frm1 = new Form(110, 25, "View products", Screen1);
            frm1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            Table tbl1 = new Table(90, 19, Screen1);
            tbl1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            tbl1.Y -= 1;

            //Add headers to the table
            tbl1.AddColumn(10, "Id");
            tbl1.AddColumn(20, "Product name");
            tbl1.AddColumn(38, "Description");
            tbl1.AddColumn(20, "Price");

            //Add rows to table
            using(DataContext context = new())
            {
                foreach (var p in context.Products)
                {
                    Row line = new Row();
                    line.AddColumn(10, p.ProductId.ToString());
                    line.AddColumn(20, p.Name);
                    line.AddColumn(38, p.Description);
                    line.AddColumn(20, Invoice.ConvertToMoney(p.Price));
                    tbl1.AddRow(line);
                }
            }

            frm1.Add(tbl1);
            Screen1.Add(frm1);

            Screen1.Render();

            // Waits user input
            tbl1.Select();

            Screen1.Remove(frm1);
        }

    }
}
