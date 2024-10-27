using BusinessObject;

namespace Repository
{
    public interface ProductRepository
    {
        List<Product> GetAll();
        void Create(Product user);
        void Update(Product user);
        void Delete(int id);
        Product FindById(int? id);
        List<Product> GetAllAvailableProducts();
        List<Product> GetNewArrivalProducts();
        List<Product> GetAllAvailableProductPagination(int pageNumber, int pageSize, int sortOrder, int? categoryId, string? productName, double? minPrice, double? maxPrice);
        List<Product> GetAllAvailableProductByCategory(int? categoryId);
        List<Product> GetAllAvailableProductByName(string? productName);
        List<Product> GetAllAvailableProductByPrice(double? minPrice, double? maxPrice);
        Product GetAllAvailableProductById(int? productId);
        List<Product> GetAllAvailableProductRelatedById(int? productId);
    }
}
