using System.Collections.Generic;
using DyShop.Data.Repositories.Cart;

namespace DyShop.Areas.Shop.Models
{
    public class CartChangeQuantityModel
    {
        public List<CartItemChangeQuantityRequest> ChangeRequests { get; set; } = new List<CartItemChangeQuantityRequest>();
    }
}