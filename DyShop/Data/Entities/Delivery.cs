using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class Delivery : IBaseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public float Price { get; set; }
    }
}