using BookStore.DataAccess.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.IModelRepository
{
    public interface ICoverTypeRepository:IRepository<CoverType>
    {
        void Update(CoverType obj);
    }
}
