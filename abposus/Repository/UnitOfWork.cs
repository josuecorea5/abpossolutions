using abposus.Data;
using abposus.Interfaces;

namespace abposus.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository ProductRepository { get; }

        public ISaleProductRepository SaleProductRepository { get; }

        public ISaleRepository SaleRepository { get; }

        public IUserRepository UserRepository  { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(context);
            SaleProductRepository = new SaleProductRepository(context);
            SaleRepository = new SaleRepository(context);
            UserRepository = new UserRepository(context);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
