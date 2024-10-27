using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class ProductRepositoryImpl : ProductRepository
    {
        private static List<Product> products = new List<Product>();
        public void Create(Product product)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var context = new DBContext())
                {
                    Product product = context.Products.SingleOrDefault(u => u.ProductId == id);
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product FindById(int? id)
        {
            Product product = null;
            try
            {
                using (var context = new DBContext())
                {
                    product = context.Products.Include(p => p.Category).SingleOrDefault(a => a.ProductId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public List<Product> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetAllAvailableProductByCategory(int? categoryId)
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetAllAvailableProductById(int? productId)
        {
            Product product = new Product();
            try
            {
                using (var context = new DBContext())
                {
                    product = context.Products.Include(p => p.Category).SingleOrDefault(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.ProductId == productId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public List<Product> GetAllAvailableProductByName(string? productName)
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.Include(p => p.Category).Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.ProductName.Contains(productName)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetAllAvailableProductByPrice(double? minPrice, double? maxPrice)
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.Include(p => p.Category).Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetAllAvailableProductPagination(int pageNumber, int pageSize, int sortOrder, int? categoryId, string? productName, double? minPrice, double? maxPrice)
        {
            try
            {
                using (var context = new DBContext())
                {
                    if(sortOrder == 1)
                    {
                        if(categoryId == null && productName == null)
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.UnitPrice)
                            .ToList();
                        }
                        else if(categoryId != null && productName == null){
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.CategoryId == categoryId)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.UnitPrice)
                            .ToList();
                        }
                        else if (categoryId == null && productName != null)
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.ProductName.Contains(productName))
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.UnitPrice)
                            .ToList();
                        }
                        else
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.CategoryId == categoryId && p.ProductName.Contains(productName))
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.UnitPrice)
                            .ToList();
                        }
                        
                    }
                    else
                    {
                        if (categoryId == null && productName == null)
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(p => p.UnitPrice)
                            .ToList();
                        }
                        else if (categoryId != null && productName == null)
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.CategoryId == categoryId)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(p => p.UnitPrice)
                            .ToList();
                        }
                        else if (categoryId == null && productName != null)
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.ProductName.Contains(productName))
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(p => p.UnitPrice)
                            .ToList();
                        }
                        else
                        {
                            products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.CategoryId == categoryId && p.ProductName.Contains(productName))
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(p => p.UnitPrice)
                            .ToList();
                        }

                    }
                    if(minPrice != null && maxPrice != null)
                    {
                        products = context.Products.Include(p => p.Category)
                            .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.UnitPrice)
                            .ToList();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetAllAvailableProductRelatedById(int? productId)
        {
            List<Product> productRelatedList = new List<Product>();
            try
            {
                using (var context = new DBContext())
                {
                    productRelatedList = context.Products.Include(p => p.Category)
                        .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false && p.ProductId != productId)
                        .Take(4)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return productRelatedList;
        }

        public List<Product> GetAllAvailableProducts()
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.Include(p => p.Category)
                        .Where(p => p.NumberOfStock > 0 && p.IsDeleted == false)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Product> GetNewArrivalProducts()
        {
            try
            {
                using (var context = new DBContext())
                {
                    products = context.Products.Include(p => p.Category).OrderByDescending(p => p.CreatedAt).Take(8).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public void Update(Product user)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry<Product>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
