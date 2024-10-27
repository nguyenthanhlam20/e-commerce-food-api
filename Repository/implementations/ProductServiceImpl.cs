using BusinessObject;
using Microsoft.Data.SqlClient;
using Repository;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class ProductServiceImpl : ProductService
    {
        private static ProductRepository productRepository = new ProductRepositoryImpl();

        public void Create(Product product)
        {
            productRepository.Create(product);
        }

        public void Delete(int id)
        {
            productRepository.Delete(id);
        }

        public Product FindById(int? productId)
        {
            return productRepository.FindById(productId);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public List<Product> GetAllAvailableProduct()
        {
            return productRepository.GetAllAvailableProducts();
        }

        public List<Product> GetAllAvailableProductByCategory(int? categoryId)
        {
            return productRepository.GetAllAvailableProductByCategory(categoryId);
        }

        public Product GetAllAvailableProductById(int? productId)
        {
            return productRepository.GetAllAvailableProductById(productId);
        }

        public List<Product> GetAllAvailableProductByName(string? productName)
        {
            return productRepository.GetAllAvailableProductByName(productName);
        }

        public List<Product> GetAllAvailableProductByPrice(double? minPrice, double? maxPrice)
        {
            return productRepository.GetAllAvailableProductByPrice(minPrice, maxPrice);
        }

        public List<Product> GetAllAvailableProductPagination(int pageNumber, int pageSize, int sortOrder, int? categoryId, string? productName, double? minPrice, double? maxPrice)
        {
            return productRepository.GetAllAvailableProductPagination(pageNumber, pageSize, sortOrder, categoryId, productName, minPrice, maxPrice);
        }

        public List<Product> GetAllAvailableProductRelatedById(int? productId)
        {
            return productRepository.GetAllAvailableProductRelatedById(productId);
        }

        public List<Product> GetNewArrivalProduct()
        {
            return productRepository.GetNewArrivalProducts();
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }
    }
}
