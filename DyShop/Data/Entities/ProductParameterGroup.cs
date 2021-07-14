using System.Collections.Generic;
using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class ProductParameterGroup : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public virtual ICollection<ProductParameter> Parameters { get; set; } = new List<ProductParameter>();
    }
}