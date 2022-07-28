using BookStore.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BookStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDatabaseContext _db;
        internal DbSet<T> dbset;
        public Repository(AppDatabaseContext db)
        {
            _db = db;
            _db.Products.Include(u => u.Category).Include(u => u.CoverType);
            this.dbset = _db.Set<T>();

        }
        public void Add(T Entity)
        {
            dbset.Add(Entity);
        }

        public void AddRange(IEnumerable<T> EntityCollection)
        {
            dbset.AddRange(EntityCollection);
        }


        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> AllEntityInstance = dbset;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    AllEntityInstance = AllEntityInstance.Include(includeProp);
                }
            }

            IQueryable<T> FilterdInstances = AllEntityInstance.Where(filter);
            return FilterdInstances.FirstOrDefault();
        }
        
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> AllEntityInstance = dbset;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    AllEntityInstance = AllEntityInstance.Include(includeProp);
                }
            }
            return AllEntityInstance.ToList();
        }

        public void Remove(T Entity)
        {
            dbset.Remove(Entity);
        }

        public void RemoveRange(IEnumerable<T> EntityCollection)
        {
            dbset.RemoveRange(EntityCollection);
        }
    }
}
