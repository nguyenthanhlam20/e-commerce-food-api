﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241026181912_Initialization")]
    partial class Initialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObject.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BusinessObject.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("BusinessObject.CartItem", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CartId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("BusinessObject.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryDescription = "Fresh and seasonal fruits like apples, bananas, and berries.",
                            CategoryImage = "https://t4.ftcdn.net/jpg/00/65/70/65/360_F_65706597_uNm2SwlPIuNUDuMwo6stBd81e25Y8K8s.jpg",
                            CategoryName = "Fruits"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryDescription = "Leafy greens, root vegetables, and other fresh produce.",
                            CategoryImage = "https://www.bhg.com/thmb/Mwd_YEkDbVg_fPsUDcWr3eZk9W0=/5645x0/filters:no_upscale():strip_icc()/difference-between-fruits-vegetables-01-5f92e7ec706b463287bcfb46985698f9.jpg",
                            CategoryName = "Vegetables"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryDescription = "Milk, cheese, yogurt, and other dairy products.",
                            CategoryImage = "https://media.istockphoto.com/id/544807136/photo/various-fresh-dairy-products.jpg?s=612x612&w=0&k=20&c=U5T70bi24itoTDive1CVonJbJ97ChyL2Pz1I2kOoSRo=",
                            CategoryName = "Dairy"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryDescription = "Fresh and processed meats like chicken, beef, and pork.",
                            CategoryImage = "https://images.squarespace-cdn.com/content/v1/59f0e6beace8641044d76e9c/1669587646206-6Z76MY4X3GBFKIUQZJ4R/Social+Meat.jpeg?format=500w",
                            CategoryName = "Meat"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryDescription = "Fish, shellfish, and other seafood items.",
                            CategoryImage = "https://myfoodbook.com.au/sites/default/files/styles/4x3/public/collections_image/great%20australian%20seafood%20-%20Easy%20As%20Aussie%20Seafood%20Platter.png",
                            CategoryName = "Seafood"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryDescription = "Drinks including soft drinks, juices, and teas.",
                            CategoryImage = "https://thumbs.dreamstime.com/b/three-fizzy-beverages-lime-ice-tall-glasses-filled-colorful-slices-creating-refreshing-vibrant-display-326097624.jpg",
                            CategoryName = "Beverages"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryDescription = "Quick bites like chips, cookies, and candies.",
                            CategoryImage = "https://media.istockphoto.com/id/1263686908/photo/mixed-salty-snack-flat-lay-table-scene-on-a-wood-background.jpg?s=612x612&w=0&k=20&c=rCZ-gpvz--NpeNA0cYGCyJj3EK0kFUSkvdsow9u4I3o=",
                            CategoryName = "Snacks"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryDescription = "Breads, pastries, cakes, and other baked goods.",
                            CategoryImage = "https://media.licdn.com/dms/image/D4D12AQE7YBHpr8T5Zw/article-cover_image-shrink_720_1280/0/1704783850526?e=2147483647&v=beta&t=8YlfaaoJZD9bhM1tEAWhnzGFNRu3NYXp7d7h-IiHJIA",
                            CategoryName = "Bakery"
                        });
                });

            modelBuilder.Entity("BusinessObject.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int?>("ApproverId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RequireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("ShipPrice")
                        .HasColumnType("float");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BusinessObject.OrderDetail", b =>
                {
                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("BusinessObject.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("BrandName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("NumberOfStock")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            BrandName = "Nature's Best",
                            CategoryId = 1,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1753),
                            Description = "Fresh red apples",
                            IsDeleted = false,
                            NumberOfStock = 100,
                            ProductImage = "https://media.istockphoto.com/id/622795204/photo/red-delicious-apples.jpg?s=612x612&w=0&k=20&c=-uu4u0m62t1nz3kcOVkallOgSWoOj_KtcVTCIlyfpXY=",
                            ProductName = "Apple",
                            UnitPrice = 1.5m
                        },
                        new
                        {
                            ProductId = 2,
                            BrandName = "Green Valley",
                            CategoryId = 1,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1755),
                            Description = "Organic bananas",
                            IsDeleted = false,
                            NumberOfStock = 150,
                            ProductImage = "https://greenspoon.co.ke/wp-content/uploads/2021/11/Greenspoon-Kwik-Basket-Sweet-Banana.jpg",
                            ProductName = "Banana",
                            UnitPrice = 0.75m
                        },
                        new
                        {
                            ProductId = 3,
                            BrandName = "Veggie Delight",
                            CategoryId = 2,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1758),
                            Description = "Fresh green broccoli",
                            IsDeleted = false,
                            NumberOfStock = 80,
                            ProductImage = "https://cdn.pixabay.com/photo/2016/03/05/19/02/broccoli-1238250_640.jpg",
                            ProductName = "Broccoli",
                            UnitPrice = 2m
                        },
                        new
                        {
                            ProductId = 4,
                            BrandName = "Farm Fresh",
                            CategoryId = 2,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1761),
                            Description = "Organic carrots",
                            IsDeleted = false,
                            NumberOfStock = 200,
                            ProductImage = "https://static.toiimg.com/photo/105672842.cms",
                            ProductName = "Carrot",
                            UnitPrice = 1.2m
                        },
                        new
                        {
                            ProductId = 5,
                            BrandName = "Dairyland",
                            CategoryId = 3,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1762),
                            Description = "Whole milk, 1 liter",
                            IsDeleted = false,
                            NumberOfStock = 500,
                            ProductImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7QU02RlLJlieaUUy7vMVMCZ_Y0ZJsX_MIUw&s",
                            ProductName = "Milk",
                            UnitPrice = 1.5m
                        },
                        new
                        {
                            ProductId = 6,
                            BrandName = "Cheese Factory",
                            CategoryId = 3,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1763),
                            Description = "Cheddar cheese block, 500g",
                            IsDeleted = false,
                            NumberOfStock = 120,
                            ProductImage = "https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/AN_images/healthiest-cheese-1296x728-swiss.jpg?w=1155&h=1528",
                            ProductName = "Cheese",
                            UnitPrice = 5m
                        },
                        new
                        {
                            ProductId = 7,
                            BrandName = "Poultry Farm",
                            CategoryId = 4,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1765),
                            Description = "Fresh chicken breast",
                            IsDeleted = false,
                            NumberOfStock = 70,
                            ProductImage = "https://www.allrecipes.com/thmb/Z5s08uvHYI2dg5LAd0vwoA47Ngo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/240208_simplebakedchickenbreasts_mfs_step1_0181-1-4x3-250b3f145a194aa3bab88e94e3cbae73.jpg",
                            ProductName = "Chicken Breast",
                            UnitPrice = 7.5m
                        },
                        new
                        {
                            ProductId = 8,
                            BrandName = "Beef Masters",
                            CategoryId = 4,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1766),
                            Description = "Lean ground beef",
                            IsDeleted = false,
                            NumberOfStock = 90,
                            ProductImage = "https://www.billyparisi.com/wp-content/uploads/2023/07/homemade-ground-beef-1.jpg",
                            ProductName = "Ground Beef",
                            UnitPrice = 6m
                        },
                        new
                        {
                            ProductId = 9,
                            BrandName = "Ocean Harvest",
                            CategoryId = 5,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1767),
                            Description = "Fresh Atlantic salmon",
                            IsDeleted = false,
                            NumberOfStock = 50,
                            ProductImage = "https://www.fromnorway.com/siteassets/species/salmon/header-salmon.jpg?width=1100&height=984&transform=downFill&hash=86d6b717a41a007814708d1cdcb8366d",
                            ProductName = "Salmon",
                            UnitPrice = 12m
                        },
                        new
                        {
                            ProductId = 10,
                            BrandName = "Citrus World",
                            CategoryId = 6,
                            CreatedAt = new DateTime(2024, 10, 27, 1, 19, 12, 676, DateTimeKind.Local).AddTicks(1769),
                            Description = "Freshly squeezed orange juice",
                            IsDeleted = false,
                            NumberOfStock = 300,
                            ProductImage = "https://media-cldnry.s-nbcnews.com/image/upload/rockcms/2024-03/orange-juice-1-jp-240311-1e99ea.jpg",
                            ProductName = "Orange Juice",
                            UnitPrice = 3m
                        });
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "USER"
                        });
                });

            modelBuilder.Entity("BusinessObject.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Gender")
                        .HasMaxLength(200)
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessObject.Cart", b =>
                {
                    b.HasOne("BusinessObject.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.CartItem", b =>
                {
                    b.HasOne("BusinessObject.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Product", "Products")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("BusinessObject.Order", b =>
                {
                    b.HasOne("BusinessObject.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObject.OrderDetail", b =>
                {
                    b.HasOne("BusinessObject.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessObject.Product", b =>
                {
                    b.HasOne("BusinessObject.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BusinessObject.User", b =>
                {
                    b.HasOne("BusinessObject.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("BusinessObject.User", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObject.Account", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("BusinessObject.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObject.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
