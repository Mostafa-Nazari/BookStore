using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.ModelRepository
{
    public class CoverTypeRepository:Repository<CoverType>,ICoverTypeRepository
    {
        private readonly DatabaseContext _db;
        public CoverTypeRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
