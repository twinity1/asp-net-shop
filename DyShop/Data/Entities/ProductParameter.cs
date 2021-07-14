using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class ProductParameter : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public virtual ProductParameterGroup Group { get; set; }
        
        public int GroupId { get; set; }
    }
}