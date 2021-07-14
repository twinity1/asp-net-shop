using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class ProductCategory : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
    }
}