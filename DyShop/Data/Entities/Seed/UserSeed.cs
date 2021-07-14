using Microsoft.AspNetCore.Identity;

namespace DyShop.Data.Entities.Seed
{
    public class UserSeed
    {
        public User[] Seed()
        {
            var user = new User
            {
                Email = "admin@dyshop.cz",
                NormalizedEmail = "ADMIN@DYSHOP.CZ",
                UserName = "admin@dyshop.cz",
                NormalizedUserName = "ADMIN@DYSHOP.CZ",
                EmailConfirmed = true,
                LockoutEnabled = true,
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "test123");
            
            return new[]
            {
                user
            };
        }
    }
}