using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.ModelRepository
{
    public class CoverTypeRepository:Repository<CoverType>,ICoverTypeRepository
    {
        private readonly AppDatabaseContext _db;
        public CoverTypeRepository(AppDatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
