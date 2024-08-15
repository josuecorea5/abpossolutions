namespace abposus.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ISaleProductRepository SaleProductRepository { get; }
        ISaleRepository SaleRepository { get; }
        IUserRepository UserRepository { get; }

        bool Save();
    }
}
