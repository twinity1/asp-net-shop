using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class ProductPhoto : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string SavePath { get; set; }
        
        public virtual Product Product { get; set; }
    }
}