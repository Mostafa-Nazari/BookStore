using BookStore.DataAccess.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.IModelRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product obj);
    }
}
