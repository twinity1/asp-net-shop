using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Repositories
{
    public class ProductParameterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductParameterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ProductParameterGroup> GetAllGroups()
        {
            return _dbContext
                .ProductParameterGroups
                .Include(x => x.Parameters.OrderBy(p => p.Title))
                .AsQueryable();
        }

        public Dictionary<int, int> GetGroupIdsForParameters(List<int> parameterIds)
        {
            var result = _dbContext.ProductParameters
                .Where(x => parameterIds.Contains(x.Id))
                .ToDictionary(x => x.Id, x => x.GroupId);

            return result;
        }

        public async Task Save(ProductParameterGroup productParameterGroup)
        {
            if (productParameterGroup.Id == 0)
            {
                _dbContext.ProductParameterGroups.Add(productParameterGroup);
            }

            await _dbContext.SaveChangesAsync();
        }

        public ProductParameterGroup? GetGroupById(int id)
        {
            return _dbContext.ProductParameterGroups.FirstOrDefault(x => x.Id == id);
        }

        public async Task Delete(ProductParameterGroup entity)
        {
            _dbContext.ProductParameterGroups.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public List<ProductParameter> GetParametersByIds(ICollection<int> ids)
        {
            if (ids.Count == 0)
            {
                return new List<ProductParameter>();
            }

            return _dbContext.ProductParameters.Where(x => ids.Contains(x.Id)).ToList();
        }
    }
}