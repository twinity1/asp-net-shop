using Microsoft.AspNetCore.Identity;

namespace DyShop.Data.Entities.Seed
{
    public class RoleSeed
    {
        public IdentityRole[] Seed()
        {
            return new[]
            {
                new IdentityRole
                {
                    NormalizedName = "ADMIN",
                    Name = "Admin",
                }
            };
        }
    }
}