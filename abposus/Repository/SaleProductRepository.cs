using abposus.Data;
using abposus.Interfaces;
using abposus.Models;

namespace abposus.Repository
{
    public class SaleProductRepository : ISaleProductRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(SaleProduct saleProduct)
        {
            _context.SaleProducts.Add(saleProduct);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
