using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.ModelRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _db;
        public CategoryRepository(DatabaseContext db):base(db)
        {
            _db = db;
        }
        
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
