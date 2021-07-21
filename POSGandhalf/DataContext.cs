using System;
using Microsoft.EntityFrameworkCore;
using POSClasses;

namespace POSGandhalf
{
    public class DataContext : DbContext
    {
        // Area to declare the DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1,1433; Initial Catalog=BdPOSGandhalf; User ID=sa; Password=Password1@");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create Admin user...
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Address = "NoRoad",
                    PostalCode = "9500-461",
                    City = "Ponta Delgada",
                    Phone = "912345678",
                    Email = "admin@admin.com",
                    UserActive = true,
                    UserLoginName = "admin",
                    UserPassword = "admin",
                    UserRole = Role.Administrator
                }
                );

            // Create default user...
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 2,
                    FirstName = "Celso",
                    LastName = "Silvestre",
                    Address = "Alameda de Belém",
                    PostalCode = "9500-461",
                    City = "Ponta Delgada",
                    Phone = "912152324",
                    Email = "clsoft.silvestre@gmail.com",
                    UserActive = true,
                    UserLoginName = "csilvestre",
                    UserPassword = "1234",
                    UserRole = Role.Operator
                }
                );

            // Create default Product Category
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory()
                {
                    Id = 1,
                    Description = "Mercearia",
                    SellingUnit = Unit.Kg
                }
                );

            // Create default Products
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    ProductCategoryId = 1,
                    Name = "Laranjas",
                    Description = "Laranja do algarve",
                    Price = 1.35f,
                },

                new Product()
                {
                    ProductId = 2,
                    ProductCategoryId = 1,
                    Name = "Maça",
                    Description = "Maça Golden",
                    Price = 1.05f
                }

                ) ;

            // Created default stock
            modelBuilder.Entity<Stock>().HasData(
                new Stock()
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 10,
                    LastUpdate = DateTime.Now,
                },

                new Stock()
                {
                    Id=2,
                    ProductId = 2,
                    Quantity = 20,
                    LastUpdate = DateTime.Now,
                } 
                );

            // Create default customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "João",
                    LastName = "Medeiros",
                    Address = "Rua direita",
                    City = "Ribeira Grande",
                    Email = "jmedeiros@jm.pt",
                    Phone = "999999999",
                    PostalCode = "9600-123",
                    VAT = 123123123
                }
                );

            // Create default Invoice
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice()
                {
                    Id = 1,
                    InvoiceNumber = 1,
                    CustomerId = 1,
                    UserId = 1,
                    InvoiceDate = DateTime.Now,
                }
                );

            // Create default Invoice Lines
            modelBuilder.Entity<InvoiceLine>().HasData(
                new InvoiceLine()
                {
                    Id = 1,
                    InvoiceId = 1,
                    StockId = 2,
                    Quantity = 5,
                    Total = 100
                },
                new InvoiceLine()
                {
                    Id = 2,
                    InvoiceId = 1,
                    StockId = 1,
                    Quantity = 3,
                    Total = 25
                }
                );
        }

    }
}
