namespace DyShop.Data.Entities
{
    public class ProductParameterRelation
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        
        public virtual ProductParameter Parameter { get; set; }
        public int ParameterId { get; set; }
    }
}