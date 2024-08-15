using abposus.Models;

namespace abposus.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetById(int id);
        void Add(Sale sale);
        Task PaySale(int id);
    }
}
