using System.Linq;
using System.Threading.Tasks;
using DyShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save(Order order)
        {
            if (order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }

            await _dbContext.SaveChangesAsync();
        }

        public Order? GetByHash(string orderHash)
        {
            return _dbContext.Orders.FirstOrDefault(x => x.Hash == orderHash);
        }

        public IQueryable<Order> GetAll()
        {
            return _dbContext.Orders.AsQueryable();
        }

        public Order? GetById(int id)
        {
            return _dbContext
                .Orders
                .Include(x => x.DeliveryAddress)
                .AsTracking()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}