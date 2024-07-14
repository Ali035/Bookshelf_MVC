namespace Bookshelf.DataAccess.Repository.IRepositury
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public IProductRepository Product { get; }

        void Save();
    }
}
