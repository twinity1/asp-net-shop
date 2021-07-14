using System.Linq;
using System.Threading.Tasks;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Repositories;
using DyShop.Data.Repositories.Cart;
using DyShop.Helpers.Controller;
using DyShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Areas.Shop.Controllers
{
    [ApiController]
    [Area(Startup.ShopArea)]
    [Route("/Cart")]
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CartRepository _cartRepository;
        private readonly DeliveryRepository _deliveryRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly CheckoutService _checkoutService;
        private readonly OrderRepository _orderRepository;

        public CartController(
            ProductRepository productRepository,
            CartRepository cartRepository,
            DeliveryRepository deliveryRepository,
            PaymentRepository paymentRepository,
            CheckoutService checkoutService,
            OrderRepository orderRepository
            )
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _deliveryRepository = deliveryRepository;
            _paymentRepository = paymentRepository;
            _checkoutService = checkoutService;
            _orderRepository = orderRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var vm = new CartIndexViewModel
            {
                Cart = await _cartRepository.GetOrCreateCartByHash(this.CartHashCookieValue(), true)
            };

            return View(vm);
        }

        [HttpPut]
        [Route(nameof(Quantity))]
        public async Task<IActionResult> Quantity(CartChangeQuantityModel model)
        {
            var cart = await _cartRepository.ModifyQuantityMany(model.ChangeRequests, this.CartHashCookieValue());

            if (cart == null)
            {
                BadRequest();
            }
            
            return PartialView("Index", new CartIndexViewModel
            {
                Cart = cart,
            });
        }

        private async Task PopulateCheckoutModel(CartCheckoutViewModel viewModel)
        {
            var deliveries = await _deliveryRepository.GetAll().ToListAsync();
            var payments = await _paymentRepository.GetAll().ToListAsync();
            var cart = await _cartRepository.GetOrCreateCartByHash(this.CartHashCookieValue(), true);

            if (viewModel.DeliveryId == 0)
            {
                viewModel.DeliveryId = deliveries.FirstOrDefault()?.Id ?? 0;
            }
            if (viewModel.PaymentId == 0)
            {
                viewModel.PaymentId = payments.FirstOrDefault()?.Id ?? 0;
            }
            
            viewModel.Cart = cart;
            viewModel.Payments = payments;
            viewModel.Deliveries = deliveries;
            viewModel.TotalPrice = _checkoutService.CalculateTotalPrice(viewModel);
        }

        [HttpGet]
        [Route(nameof(Checkout))]
        public async Task<IActionResult> Checkout()
        {
            var model = new CartCheckoutViewModel();
            
            await PopulateCheckoutModel(model);

            return View(model);
        }

        [HttpPost]
        [Route(nameof(Checkout))]
        public async Task<IActionResult> Checkout([FromForm] CartCheckoutViewModel model)
        {
            await PopulateCheckoutModel(model);
            
            if (this.Validate(model) == false)
            {
                return View(model);
            }

            var order = _checkoutService.MapOrderFromCart(model);

            await _orderRepository.Save(order);

            await _cartRepository.RemoveCart(model.Cart);
            
            return RedirectToAction("Status", "Order", new { hash = order.Hash });
        }

        [HttpPost]
        [Route(nameof(RefreshCheckout))]
        public async Task<IActionResult> RefreshCheckout([FromForm] CartCheckoutViewModel model)
        {
            await PopulateCheckoutModel(model);
            
            ModelState.Clear();
            
            return PartialView("Checkout", model);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(CartAddModel model)
        {
            var cartId = this.CartHashCookieValue();

            var product = _productRepository.GetBySlug(model.Slug);

            if (product == null)
            {
                return Json(new {
                    CartHtml = "",
                    Success = false,
                    Message = "Produkt již není k dispozici.",
                });            
            }

            if (this.Validate(model) == false)
            {
                return Json(new {
                    CartHtml = "",
                    Success = false,
                    Message = ModelState.Values.Select(x => x.Errors).First(),
                });
            }

            await _cartRepository.Add(product, model.Quantity, this.CartHashCookieValue());
            
            return Json(new
            {
                CartHtml = await this.RenderViewComponent("cart", new {cartHash = cartId}),
                Success = true,
                Message = "Produkt byl přidán do košíku.",
            });
        }

        [HttpDelete]
        [Route("Delete/{cartItemId}")]
        public async Task<IActionResult> Delete(int cartItemId)
        {
            await _cartRepository.RemoveCartItem(this.CartHashCookieValue(), cartItemId);

            return Ok();
        }
    }
}