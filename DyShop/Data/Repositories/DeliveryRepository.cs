using System.Linq;
using DyShop.Data.Entities;

namespace DyShop.Data.Repositories
{
    public class DeliveryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeliveryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Delivery> GetAll()
        {
            return _dbContext.Deliveries.AsQueryable();
        }

        public Delivery? GetById(int id)
        {
            return _dbContext.Deliveries.FirstOrDefault(x => x.Id == id);
        }
    }
}