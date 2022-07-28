using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.ModelRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDatabaseContext _db;
        public CategoryRepository(AppDatabaseContext db):base(db)
        {
            _db = db;
        }
        
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
