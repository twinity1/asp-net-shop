using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class ClientAddress : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string City { get; set; }
        
        public string Street { get; set; }
        
        public string ZipCode { get; set; }
        
        public string? Note { get; set; }
    }
}