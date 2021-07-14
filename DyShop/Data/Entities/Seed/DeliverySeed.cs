namespace DyShop.Data.Entities.Seed
{
    public class DeliverySeed
    {
        public Delivery[] Seed()
        {
            return new[]
            {
                new Delivery
                {
                    Id = 1,
                    Title = "PPL",
                    Price = 90.0f,
                },
                new Delivery
                {
                    Id = 2,
                    Title = "Česká pošta",
                    Price = 70.0f,
                }
            };
        }

    }
}