using System.Threading.Tasks;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Controllers
{
    [Area(Startup.ShopArea)]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Status(string hash)
        {
            var order = _orderRepository.GetByHash(hash);

            if (order == null)
            {
                HttpContext.Response.StatusCode = 404;
                return View("_NotFound");
            }
            
            return View(new OrderStatusViewModel { Order = order});
        }
    }
}