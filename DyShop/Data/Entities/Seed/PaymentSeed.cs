using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Entities.Seed
{
    public class PaymentSeed
    {
        public Payment[] Seed()
        {
            return new[]
            {
                new Payment
                {
                    Id = 1,
                    Title = "Bankovní převod",
                    Price = 0.0f,
                }
            };
        }
    }
}