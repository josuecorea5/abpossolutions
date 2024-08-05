using abposus.Models;

namespace abposus.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSales();
        void Add(Sale sale);
        bool Save();
    }
}
