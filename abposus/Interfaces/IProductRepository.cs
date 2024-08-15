using abposus.Models;

namespace abposus.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
