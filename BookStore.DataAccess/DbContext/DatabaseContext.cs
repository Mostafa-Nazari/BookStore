using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
