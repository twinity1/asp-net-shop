using DyShop.Data.Entities.Types;
using Microsoft.EntityFrameworkCore.Storage;

namespace DyShop.Data.Entities
{
    public class OrderItem : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public float Price { get; set; }

        public float TotalPrice { get; set; }
        
        public float Quantity { get; set; }

        public virtual Order Order { get; set; }
        
        public virtual Product? Product { get; set; }
    }
}