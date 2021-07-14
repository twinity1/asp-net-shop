using System.Threading.Tasks;
using DyShop.Data.Repositories;
using DyShop.Data.Repositories.Cart;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Views.Shared.Components.Cart
{
    [ViewComponent(Name = "cart")]
    public class CartComponent : ViewComponent
    {
        private readonly CartRepository _cartRepository;

        public CartComponent(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? cartHash)
        {
            if (cartHash == null)
            {
                ViewData.Model = null;
                return View();
            }
            
            return View(await _cartRepository.GetByHash(cartHash));
        }
    }
}