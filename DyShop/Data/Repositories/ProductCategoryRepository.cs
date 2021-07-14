using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyShop.Data.Entities;

namespace DyShop.Data.Repositories
{
    public class ProductCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductCategory> GetAll()
        {
            return _dbContext.ProductCategories.ToList();
        }

        public ProductCategory? GetById(int id)
        {
            return _dbContext.ProductCategories.FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategory> GetByIds(ICollection<int> ids)
        {
            if (ids.Count == 0)
            {
                return new List<ProductCategory>();
            }

            return _dbContext.ProductCategories.Where(x => ids.Contains(x.Id)).ToList();
        }

        public async Task Save(ProductCategory productCategory)
        {
            if (productCategory.Id == 0)
            {
                _dbContext.ProductCategories.Add(productCategory);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                _dbContext.ProductCategories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}