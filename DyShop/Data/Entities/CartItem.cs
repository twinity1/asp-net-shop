using System.ComponentModel.DataAnnotations.Schema;
using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class CartItem : IBaseEntity
    {
        public int Id { get; set; }

        public float Quantity { get; set; }
        
        public virtual Product Product { get; set; }
        
        public virtual Cart Cart { get; set; }

        [NotMapped] public float Price => Product.Price * Quantity;
    }
}