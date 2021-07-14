using System.Diagnostics;
using System.Linq;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Repositories;
using DyShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DyShop.Areas.Shop.Controllers
{
    [Area(Startup.ShopArea)]
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;

        public HomeController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel
            {
                FeaturedProducts = _productRepository.GetFeatured().ToList(),
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}