﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POSGandhalf;

namespace POSGandhalf.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210721190004_v6")]
    partial class v6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("POSClasses.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VAT")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Rua direita",
                            City = "Ribeira Grande",
                            Email = "jmedeiros@jm.pt",
                            FirstName = "João",
                            LastName = "Medeiros",
                            Phone = "999999999",
                            PostalCode = "9600-123",
                            VAT = 123123123
                        });
                });

            modelBuilder.Entity("POSClasses.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("inProgress")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            InvoiceDate = new DateTime(2021, 7, 21, 19, 0, 3, 783, DateTimeKind.Local).AddTicks(4820),
                            InvoiceNumber = 1,
                            TotalAmount = 0.0,
                            UserId = 1,
                            inProgress = true
                        });
                });

            modelBuilder.Entity("POSClasses.InvoiceLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("StockId");

                    b.ToTable("InvoiceLines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InvoiceId = 1,
                            Quantity = 5f,
                            StockId = 2,
                            Total = 100.0
                        },
                        new
                        {
                            Id = 2,
                            InvoiceId = 1,
                            Quantity = 3f,
                            StockId = 1,
                            Total = 25.0
                        });
                });

            modelBuilder.Entity("POSClasses.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Laranja do algarve",
                            Name = "Laranjas",
                            Price = 1.35f,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Maça Golden",
                            Name = "Maça",
                            Price = 1.05f,
                            ProductCategoryId = 1
                        });
                });

            modelBuilder.Entity("POSClasses.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("DefaultTax")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellingUnit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DefaultTax = 0f,
                            Description = "Mercearia",
                            SellingUnit = 4
                        });
                });

            modelBuilder.Entity("POSClasses.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LastUpdate = new DateTime(2021, 7, 21, 19, 0, 3, 756, DateTimeKind.Local).AddTicks(9080),
                            ProductId = 1,
                            Quantity = 10f
                        },
                        new
                        {
                            Id = 2,
                            LastUpdate = new DateTime(2021, 7, 21, 19, 0, 3, 782, DateTimeKind.Local).AddTicks(9860),
                            ProductId = 2,
                            Quantity = 20f
                        });
                });

            modelBuilder.Entity("POSClasses.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UserLastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserLoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "NoRoad",
                            City = "Ponta Delgada",
                            Email = "admin@admin.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Phone = "912345678",
                            PostalCode = "9500-461",
                            UserActive = true,
                            UserLastLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserLoginName = "admin",
                            UserPassword = "admin",
                            UserRole = 3
                        },
                        new
                        {
                            Id = 2,
                            Address = "Alameda de Belém",
                            City = "Ponta Delgada",
                            Email = "clsoft.silvestre@gmail.com",
                            FirstName = "Celso",
                            LastName = "Silvestre",
                            Phone = "912152324",
                            PostalCode = "9500-461",
                            UserActive = true,
                            UserLastLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserLoginName = "csilvestre",
                            UserPassword = "1234",
                            UserRole = 1
                        });
                });

            modelBuilder.Entity("POSClasses.Invoice", b =>
                {
                    b.HasOne("POSClasses.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POSClasses.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("POSClasses.InvoiceLine", b =>
                {
                    b.HasOne("POSClasses.Invoice", "Invoice")
                        .WithMany("Lines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POSClasses.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("POSClasses.Product", b =>
                {
                    b.HasOne("POSClasses.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("POSClasses.Stock", b =>
                {
                    b.HasOne("POSClasses.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("POSClasses.Customer", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("POSClasses.Invoice", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("POSClasses.Product", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("POSClasses.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
