using BusinessObject;

namespace Service
{
    public interface ProductService
    {
        List<Product> GetAll();
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
        Product FindById(int? productId);
        List<Product> GetAllAvailableProduct();
        List<Product> GetNewArrivalProduct();
        List<Product> GetAllAvailableProductPagination(int pageNumber, int pageSize, int sortOrder, int? categoryId, string? productName, double? minPrice, double? maxPrice);
        List<Product> GetAllAvailableProductByCategory(int? categoryId);
        List<Product> GetAllAvailableProductByName(string? productName);
        List<Product> GetAllAvailableProductByPrice(double? minPrice, double? maxPrice);
        Product GetAllAvailableProductById(int? productId);
        List<Product> GetAllAvailableProductRelatedById(int? productId);
    }
}
