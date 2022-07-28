using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.DataAccess.Repository.ModelRepository;

namespace BookStore.DataAccess.Repository.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDatabaseContext _db;
        public UnitOfWork(AppDatabaseContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
