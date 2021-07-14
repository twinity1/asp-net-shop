using System.Linq;
using DyShop.Data.Entities;

namespace DyShop.Data.Repositories
{
    public class PaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Payment> GetAll()
        {
            return _dbContext.Payments.AsQueryable();
        }

        public Payment? GetById(int id)
        {
            return _dbContext.Payments.FirstOrDefault(x => x.Id == id);
        }
    }
}