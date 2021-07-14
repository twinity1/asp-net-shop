namespace DyShop.Data.Repositories.Cart
{
    public class CartItemChangeQuantityRequest
    {
        public int CartItemId { get; set; }
        
        public float Quantity { get; set; }
    }
}