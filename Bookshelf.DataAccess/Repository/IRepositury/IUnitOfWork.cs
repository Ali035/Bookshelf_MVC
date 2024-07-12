namespace Bookshelf.DataAccess.Repository.IRepositury
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        void Save();
    }
}
