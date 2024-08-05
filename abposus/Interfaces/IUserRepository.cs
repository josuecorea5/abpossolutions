using abposus.Models;

namespace abposus.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
    }
}
