namespace DyShop.Data.Entities
{
    public class ProductCategoryRelation
    {
        public virtual ProductCategory ProductCategory { get; set; }
        
        public int ProductCategoryId { get; set; }
        
        public virtual Product Product { get; set; }
        
        public int ProductId { get; set; }
    }
}