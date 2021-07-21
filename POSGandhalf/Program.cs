using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        static void OperationsMenu()
        {
            // Adds one main menu to the app and sets the events
            List<MenuItem> OperationsMenuItems = new();

            MenuItem InvoicesMnuItem = new MenuItem("Invoices");
            InvoicesMnuItem.Selected += InvoicesMnuItem_Selected;
            OperationsMenuItems.Add(InvoicesMnuItem);

            MenuItem ViewStockMnuItem = new MenuItem("View Stock");
            ViewStockMnuItem.Selected += ViewStockMnuItem_Selected;
            OperationsMenuItems.Add(ViewStockMnuItem);

            //Add Stock Management if User role is administrator or supervisor
            if(User.CurrentUser.UserRole > Role.Operator)
            {
                MenuItem StockMngMnuItem = new MenuItem("Stock Management");
                StockMngMnuItem.Selected += StockMngMnuItem_Selected;
                OperationsMenuItems.Add(StockMngMnuItem);
            }

            MenuItem BackMnuItem = new MenuItem("Back");
            BackMnuItem.Selected += OperationsBackMnuItem_Selected;
            OperationsMenuItems.Add(BackMnuItem);

            // Created the Menu for administration
            Menu LoginMnu = new Menu("Operations Menu", OperationsMenuItems);
            LoginMnu.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Set the login menu to the Screen and wait for the selection of the user
            Screen1.Add(LoginMnu);
            Screen1.Render();
            LoginMnu.Select();

        }

        static void InvoicesMenu()
        {
            // Adds one main menu to the app and sets the events
            List<MenuItem> InvoiceMenuItems = new();

            MenuItem NewInvoiceMnuItem = new MenuItem("New Invoice");
            NewInvoiceMnuItem.Selected += NewInvoiceMnuItem_Selected;
            InvoiceMenuItems.Add(NewInvoiceMnuItem);

            MenuItem ViewInvoicesMnuItem = new MenuItem("View Invoices");
            ViewInvoicesMnuItem.Selected += ViewInvoicesMnuItem_Selected;
            InvoiceMenuItems.Add(ViewInvoicesMnuItem);

            MenuItem BackMnuItem = new MenuItem("Back");
            BackMnuItem.Selected += InvoicesBackMnuItem_Selected;
            InvoiceMenuItems.Add(BackMnuItem);

            // Created the Menu for administration
            Menu InvoiceMnu = new Menu("Invoices Menu", InvoiceMenuItems);
            InvoiceMnu.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            // Set the login menu to the Screen and wait for the selection of the user
            Screen1.Add(InvoiceMnu);
            Screen1.Render();
            InvoiceMnu.Select();

        }

        private static void OperationsBackMnuItem_Selected(object sender, EventArgs e)
        {
            //Do noting and get back
            AdministrationMenu();
        }

        private static void InvoicesBackMnuItem_Selected(object sender, EventArgs e)
        {
            //Do noting and get back
            OperationsMenu();
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
                        OperationsMenu();
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
            tbl1.AddColumn(5, "Id");
            tbl1.AddColumn(20, "Product name");
            tbl1.AddColumn(38, "Description");
            tbl1.AddColumn(15, "Price");
            tbl1.AddColumn(10, "Qty.");

            //Add rows to table
            using (DataContext context = new())
            {
                // Loading Associated Product (Eager Loading)
                // Check: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

                var products = context.Stocks
                    .Include(prod => prod.Product)
                    .ToList();

                foreach (var p in context.Stocks)
                {
                    Row line = new Row();
                    line.AddColumn(5, p.ProductId.ToString());
                    line.AddColumn(20, p.Product.Name);
                    line.AddColumn(38, p.Product.Description);
                    line.AddColumn(20, Invoice.ConvertToMoney(p.Product.Price));
                    line.AddColumn(10, p.Quantity.ToString());
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

        private static void StockMngMnuItem_Selected(object sender, EventArgs e)
        {
            // View products list
            Form frm1 = new Form(110, 25, "Stock management", Screen1);
            frm1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            Table tbl1 = new Table(90, 19, Screen1);
            tbl1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            tbl1.Y -= 1;

            //Add headers to the table
            tbl1.AddColumn(5, "Id");
            tbl1.AddColumn(20, "Product name");
            tbl1.AddColumn(38, "Description");
            tbl1.AddColumn(15, "Price");
            tbl1.AddColumn(10, "Qty.");

            //Add rows to table
            using (DataContext context = new())
            {
                // Loading Associated Product (Eager Loading)
                // Check: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

                var products = context.Stocks
                    .Include(prod => prod.Product)
                    .ToList();

                foreach (var p in context.Stocks)
                {
                    Row line = new Row();
                    line.AddColumn(5, p.ProductId.ToString());
                    line.AddColumn(20, p.Product.Name);
                    line.AddColumn(38, p.Product.Description);
                    line.AddColumn(20, Invoice.ConvertToMoney(p.Product.Price));
                    line.AddColumn(10, p.Quantity.ToString());
                    tbl1.AddRow(line);
                }
            }

            frm1.Add(tbl1);
            Screen1.Add(frm1);

            Screen1.Render();

            // Waits user input 
            string selectedId = tbl1.Select();

            // Edit the product
            while ( selectedId != null)
            {
                // Create new Form to edit the product stock and price.
                Form frmEditProduct = new Form(50, 20, "Edit product", Screen1);
                frmEditProduct.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

                TextBox txtProductId = new TextBox(50, 8, 10, "Id");
                txtProductId.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                txtProductId.Value = selectedId;

                TextBox txtProductName = new TextBox(50, 12, 15, "Product");
                txtProductName.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                txtProductName.Value = selectedId;

                TextBox txtProductPrice = new TextBox(50, 16, 10, "Price");
                txtProductPrice.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                txtProductPrice.Value = selectedId;

                TextBox txtProductStock = new TextBox(50, 20, 10, "Stock");
                txtProductStock.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                txtProductStock.Value = selectedId;


                frmEditProduct.Add(txtProductId);
                frmEditProduct.Add(txtProductName);
                frmEditProduct.Add(txtProductPrice);
                frmEditProduct.Add(txtProductStock);
                Screen1.Add(frmEditProduct);

                // Get the selected product from Database

                using (DataContext context = new())
                {
                    var products = context.Stocks
                    .Include(prod => prod.Product)
                    .ToList();

                    Stock st = context.Stocks.Find(int.Parse(selectedId));
                    if (st == null)
                    {
                        // Product not found in Database
                    } else
                    {
                        txtProductId.Value = selectedId;
                        txtProductName.Value = st.Product.Name;
                        txtProductPrice.Value = st.Product.Price.ToString();
                        txtProductStock.Value = st.Quantity.ToString();

                        Screen1.Render();
                        string newPrice = txtProductPrice.Select();
                        string newStock = txtProductStock.Select();

                        float newPriceFloat;
                        bool convertPriceOk = float.TryParse(newPrice, out newPriceFloat);

                        float newStockFloat;
                        bool convertStockOk = float.TryParse(newStock, out newStockFloat);

                        if (convertPriceOk)
                            st.Product.Price = newPriceFloat;

                        if (convertStockOk)
                            st.UpdateQuantity(newStockFloat);

                        // TODO: Needs to update the cell in the table because the data is not refreshed from datatable
                        //Find the line were the ID is == selectedID
                        foreach(Row r in tbl1.TableRows)
                        {
                            if(r.Columns[0].Title == selectedId)
                            {
                                //This is the correct row...
                                if(convertPriceOk)
                                    r.Columns[3].Title = Invoice.ConvertToMoney(newPriceFloat);
                                if(convertStockOk)
                                    r.Columns[4].Title = newStock;
                            }
                        }

                        context.SaveChanges();

                    }
                }
 
                Screen1.Remove(frmEditProduct);
                Screen1.Render();
                selectedId = tbl1.Select();
            }

            Screen1.Remove(frm1);
        }

        private static void InvoicesMnuItem_Selected(object sender, EventArgs e)
        {
            InvoicesMenu();
        }

        private static void NewInvoiceMnuItem_Selected(object sender, EventArgs e)
        {

        }

        private static void ViewInvoicesMnuItem_Selected(object sender, EventArgs e)
        {
            // View products list
            Form frm1 = new Form(110, 25, "View Invoices", Screen1);
            frm1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

            Table tbl1 = new Table(90, 19, Screen1);
            tbl1.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
            tbl1.Y -= 1;

            //Add headers to the table
            tbl1.AddColumn(10, "Id");
            tbl1.AddColumn(20, "Date");
            tbl1.AddColumn(38, "Client");
            tbl1.AddColumn(20, "Amount");

            //Add rows to table
            using (DataContext context = new())
            {
                // Loading Associated Product (Eager Loading)
                // Check: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

                var clients = context.Invoices.Include(cli => cli.Customer).ToList();

                foreach (var p in context.Invoices)
                {
                    Row line = new Row();
                    line.AddColumn(10, p.Id.ToString());
                    line.AddColumn(20, p.InvoiceDate.ToShortDateString());
                    line.AddColumn(38, p.Customer.FirstName + " " + p.Customer.LastName);
                    line.AddColumn(20, Invoice.ConvertToMoney(p.TotalAmount));
                    tbl1.AddRow(line);
                }
            }

            frm1.Add(tbl1);
            Screen1.Add(frm1);

            Screen1.Render();

            // Waits user input
            string SelectedInvoiceId = tbl1.Select();

            while (SelectedInvoiceId != null)
            {
                // TODO: Make code to view the Invoice details
                Form frmViewInvoice = new Form(115, 28, "View Invoice details", Screen1);
                frmViewInvoice.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

                // Fields for Invoice data (number, customer, date, etc...)
                TextBox TxtInvoiceNum = new TextBox(10, 3, 18, "Invoice number");
                TxtInvoiceNum.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

                TextBox TxtInvoiceDate = new TextBox(30, 3, 18, "Invoice Date");
                TxtInvoiceDate.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

                TextBox TxtInvoiceCustomer = new TextBox(50, 3, 30, "Customer");
                TxtInvoiceCustomer.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);

                frmViewInvoice.Add(TxtInvoiceNum);
                frmViewInvoice.Add(TxtInvoiceDate);
                frmViewInvoice.Add(TxtInvoiceCustomer);

                Table TblDetail = new Table(100, 18, Screen1);
                TblDetail.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                TblDetail.Y += 2;
                TblDetail.AddColumn(20, "Category");
                TblDetail.AddColumn(40, "Description");
                TblDetail.AddColumn(10, "Quantity");
                TblDetail.AddColumn(10, "Tax");
                TblDetail.AddColumn(20, "Amount");

                Label lblInfo = new Label(81, 27, "Press one Key to continue...");
                lblInfo.SetColors(PRIMARY_COLOR, SECONDARY_COLOR);
                frmViewInvoice.Add(lblInfo);


                // Get Data from Database and populate Widget fiels
                using (DataContext context = new())
                {
                    // Loading Associated Product (Eager Loading)
                    // Check: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

                    var lines = context.Invoices.Include(lin => lin.Lines).ToList();
                    var customer = context.Invoices.Include(lin => lin.Customer).ToList();
                    var stock = context.InvoiceLines.Include(lin => lin.Stock).ToList();
                    var product = context.Stocks.Include(lin => lin.Product).ToList();
                    var description = context.Products.Include(lin => lin.Category).ToList();


                    Invoice inv = context.Invoices.Find(int.Parse(SelectedInvoiceId));

                    TxtInvoiceNum.Value = inv.Id.ToString();
                    TxtInvoiceDate.Value = inv.InvoiceDate.ToShortDateString();
                    TxtInvoiceCustomer.Value = inv.Customer.FirstName + " " + inv.Customer.LastName;

                    foreach (var item in inv.Lines)
                    {
                        Row line = new Row();
                        line.AddColumn(20, item.Stock.Product.Category.Description.ToString());
                        line.AddColumn(40, item.Stock.Product.Name + " - " + item.Stock.Product.Description);
                        line.AddColumn(10, item.Quantity.ToString());
                        line.AddColumn(10, Invoice.ConvertToPercentage(item.Stock.Product.Category.DefaultTax));
                        line.AddColumn(20, Invoice.ConvertToMoney(item.Total));
                        TblDetail.AddRow(line);
                    }

                }
                frmViewInvoice.Add(TblDetail);
                Screen1.Add(frmViewInvoice);
                Screen1.Render();
                Console.ReadKey();
                Screen1.Remove(frmViewInvoice);
                Screen1.Render();
                SelectedInvoiceId = tbl1.Select();
            }

            Screen1.Remove(frm1);

        }

    }
}
