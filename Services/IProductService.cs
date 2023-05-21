using MavickBackend.Models;
using MavickBackend.Models.Request;

namespace MavickBackend.Services
{
    public interface IProductService
    {
        public Product Add(ProductRequest model);
        public Product? Update(ProductRequest model);
        public List<Product> GetAll();
        public Product? GetProductById(int id);
        public List<Product>? GetProductWithCategory(string cate);
        public List<Product>? GetProductWithSize(string size);
        public List<Product>? GetProductWithCategoryAndSize(string cate, string size);
        public Product? DeleteProduct(int id);
    }
}
