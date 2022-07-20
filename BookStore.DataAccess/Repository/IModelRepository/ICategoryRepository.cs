using BookStore.DataAccess.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.IModelRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category obj);
    }
}
