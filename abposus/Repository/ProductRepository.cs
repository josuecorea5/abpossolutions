using abposus.Data;
using abposus.Interfaces;
using abposus.Models;
using Microsoft.EntityFrameworkCore;

namespace abposus.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.Products.Update(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
