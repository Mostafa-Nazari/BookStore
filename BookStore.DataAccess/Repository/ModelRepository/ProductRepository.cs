using BookStore.DataAccess.Repository.IModelRepository;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository.ModelRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DatabaseContext _db;
        public ProductRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            Product objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Author = obj.Author;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if (obj.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;
                }
            }

        }
    }
}
