using System.Linq.Expressions;

namespace BookStore.DataAccess.IRepository
{
    public interface IRepository<T> where T:class
    { 
        T Get(Expression<Func<T,bool>> filter,string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T Entity);
        void AddRange(IEnumerable<T> EntityCollection);
        void Remove(T Entity);
        void RemoveRange(IEnumerable<T> EntityCollection);
    }
}
