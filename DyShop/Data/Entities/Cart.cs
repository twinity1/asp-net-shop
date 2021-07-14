using System.Collections.Generic;
using System.Linq;
using DyShop.Data.Entities.Types;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Entities
{
    [Index(nameof(Hash))]
    public class Cart : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Hash { get; set; }

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public float TotalPrice()
        {
            return Items.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}