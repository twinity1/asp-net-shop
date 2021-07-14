using DyShop.Data.Entities;
using DyShop.Data.Entities.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        
        public DbSet<Cart> Carts { get; set; }
        
        public DbSet<CartItem> CartItems { get; set; }
        
        public DbSet<ClientAddress> ClientAddresses { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }
        
        public DbSet<Delivery> Deliveries { get; set; }
        
        public DbSet<Payment> Payments { get; set; }
        
        public DbSet<ProductCategory> ProductCategories { get; set; }
        
        public DbSet<ProductCategoryRelation> ProductCategoryRelations { get; set; }
        
        public DbSet<ProductParameterGroup> ProductParameterGroups { get; set; }
        
        public DbSet<ProductParameter> ProductParameters { get; set; }
        
        public DbSet<ProductParameterRelation> ProductParameterRelations { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ProductCategoryRelation>().HasKey(sc => new { sc.ProductId, sc.ProductCategoryId });
            builder.Entity<ProductParameterRelation>().HasKey(sc => new { sc.ProductId, sc.ParameterId });
            builder.Entity<CartItem>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.SetNull);
                
            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            var roles = new RoleSeed().Seed();
            var users = new UserSeed().Seed();
            var deliveries = new DeliverySeed().Seed();
            var payments = new PaymentSeed().Seed();
            
            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<User>().HasData(users);
            builder.Entity<Delivery>().HasData(deliveries);
            builder.Entity<Payment>().HasData(payments);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roles[0].Id, 
                UserId = users[0].Id,
            });
        }
    }
}