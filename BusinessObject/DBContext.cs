using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class DBContext : DbContext
    {
        public DBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartId, c.ProductId });

            modelBuilder.Entity<OrderDetail>().HasKey(c => new { c.OrderId, c.ProductId });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "ADMIN" },
                new Role { RoleId = 2, RoleName = "USER" }
                );

            SeedCategories(modelBuilder);
            SeedProducts(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, CategoryName = "Fruits", CategoryDescription = "Fresh and seasonal fruits like apples, bananas, and berries.", CategoryImage = "https://t4.ftcdn.net/jpg/00/65/70/65/360_F_65706597_uNm2SwlPIuNUDuMwo6stBd81e25Y8K8s.jpg" },
              new Category { CategoryId = 2, CategoryName = "Vegetables", CategoryDescription = "Leafy greens, root vegetables, and other fresh produce.", CategoryImage = "https://www.bhg.com/thmb/Mwd_YEkDbVg_fPsUDcWr3eZk9W0=/5645x0/filters:no_upscale():strip_icc()/difference-between-fruits-vegetables-01-5f92e7ec706b463287bcfb46985698f9.jpg" },
              new Category { CategoryId = 3, CategoryName = "Dairy", CategoryDescription = "Milk, cheese, yogurt, and other dairy products.", CategoryImage = "https://media.istockphoto.com/id/544807136/photo/various-fresh-dairy-products.jpg?s=612x612&w=0&k=20&c=U5T70bi24itoTDive1CVonJbJ97ChyL2Pz1I2kOoSRo=" },
              new Category { CategoryId = 4, CategoryName = "Meat", CategoryDescription = "Fresh and processed meats like chicken, beef, and pork.", CategoryImage = "https://images.squarespace-cdn.com/content/v1/59f0e6beace8641044d76e9c/1669587646206-6Z76MY4X3GBFKIUQZJ4R/Social+Meat.jpeg?format=500w" },
              new Category { CategoryId = 5, CategoryName = "Seafood", CategoryDescription = "Fish, shellfish, and other seafood items.", CategoryImage = "https://myfoodbook.com.au/sites/default/files/styles/4x3/public/collections_image/great%20australian%20seafood%20-%20Easy%20As%20Aussie%20Seafood%20Platter.png" },
              new Category { CategoryId = 6, CategoryName = "Beverages", CategoryDescription = "Drinks including soft drinks, juices, and teas.", CategoryImage = "https://thumbs.dreamstime.com/b/three-fizzy-beverages-lime-ice-tall-glasses-filled-colorful-slices-creating-refreshing-vibrant-display-326097624.jpg" },
              new Category { CategoryId = 7, CategoryName = "Snacks", CategoryDescription = "Quick bites like chips, cookies, and candies.", CategoryImage = "https://media.istockphoto.com/id/1263686908/photo/mixed-salty-snack-flat-lay-table-scene-on-a-wood-background.jpg?s=612x612&w=0&k=20&c=rCZ-gpvz--NpeNA0cYGCyJj3EK0kFUSkvdsow9u4I3o=" },
              new Category { CategoryId = 8, CategoryName = "Bakery", CategoryDescription = "Breads, pastries, cakes, and other baked goods.", CategoryImage = "https://media.licdn.com/dms/image/D4D12AQE7YBHpr8T5Zw/article-cover_image-shrink_720_1280/0/1704783850526?e=2147483647&v=beta&t=8YlfaaoJZD9bhM1tEAWhnzGFNRu3NYXp7d7h-IiHJIA" }
              );
        }


        public static void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    ProductName = "Apple",
                    ProductImage = "https://media.istockphoto.com/id/622795204/photo/red-delicious-apples.jpg?s=612x612&w=0&k=20&c=-uu4u0m62t1nz3kcOVkallOgSWoOj_KtcVTCIlyfpXY=",
                    Description = "Fresh red apples",
                    BrandName = "Nature's Best",
                    UnitPrice = 1.50,
                    NumberOfStock = 100,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 1,
                    ProductName = "Banana",
                    ProductImage = "https://greenspoon.co.ke/wp-content/uploads/2021/11/Greenspoon-Kwik-Basket-Sweet-Banana.jpg",
                    Description = "Organic bananas",
                    BrandName = "Green Valley",
                    UnitPrice = 0.75,
                    NumberOfStock = 150,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 2,
                    ProductName = "Broccoli",
                    ProductImage = "https://cdn.pixabay.com/photo/2016/03/05/19/02/broccoli-1238250_640.jpg",
                    Description = "Fresh green broccoli",
                    BrandName = "Veggie Delight",
                    UnitPrice = 2.00,
                    NumberOfStock = 80,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 2,
                    ProductName = "Carrot",
                    ProductImage = "https://static.toiimg.com/photo/105672842.cms",
                    Description = "Organic carrots",
                    BrandName = "Farm Fresh",
                    UnitPrice = 1.20,
                    NumberOfStock = 200,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 3,
                    ProductName = "Milk",
                    ProductImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7QU02RlLJlieaUUy7vMVMCZ_Y0ZJsX_MIUw&s",
                    Description = "Whole milk, 1 liter",
                    BrandName = "Dairyland",
                    UnitPrice = 1.50,
                    NumberOfStock = 500,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 3,
                    ProductName = "Cheese",
                    ProductImage = "https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/AN_images/healthiest-cheese-1296x728-swiss.jpg?w=1155&h=1528",
                    Description = "Cheddar cheese block, 500g",
                    BrandName = "Cheese Factory",
                    UnitPrice = 5.00,
                    NumberOfStock = 120,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 7,
                    CategoryId = 4,
                    ProductName = "Chicken Breast",
                    ProductImage = "https://www.allrecipes.com/thmb/Z5s08uvHYI2dg5LAd0vwoA47Ngo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/240208_simplebakedchickenbreasts_mfs_step1_0181-1-4x3-250b3f145a194aa3bab88e94e3cbae73.jpg",
                    Description = "Fresh chicken breast",
                    BrandName = "Poultry Farm",
                    UnitPrice = 7.50,
                    NumberOfStock = 70,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 8,
                    CategoryId = 4,
                    ProductName = "Ground Beef",
                    ProductImage = "https://www.billyparisi.com/wp-content/uploads/2023/07/homemade-ground-beef-1.jpg",
                    Description = "Lean ground beef",
                    BrandName = "Beef Masters",
                    UnitPrice = 6.00,
                    NumberOfStock = 90,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 9,
                    CategoryId = 5,
                    ProductName = "Salmon",
                    ProductImage = "https://www.fromnorway.com/siteassets/species/salmon/header-salmon.jpg?width=1100&height=984&transform=downFill&hash=86d6b717a41a007814708d1cdcb8366d",
                    Description = "Fresh Atlantic salmon",
                    BrandName = "Ocean Harvest",
                    UnitPrice = 12.00,
                    NumberOfStock = 50,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    ProductId = 10,
                    CategoryId = 6,
                    ProductName = "Orange Juice",
                    ProductImage = "https://media-cldnry.s-nbcnews.com/image/upload/rockcms/2024-03/orange-juice-1-jp-240311-1e99ea.jpg",
                    Description = "Freshly squeezed orange juice",
                    BrandName = "Citrus World",
                    UnitPrice = 3.00,
                    NumberOfStock = 300,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                }
            );

        }
    }
}
