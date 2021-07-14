using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Entities.Enums;
using DyShop.Data.Repositories;
using DyShop.Helpers.Controller;
using DyShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace DyShop.Areas.Admin.Controllers
{
    [Area(Startup.AdminArea)]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly DeliveryRepository _deliveryRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFlashMessage _flashMessage;

        public OrderController(
            OrderRepository orderRepository,
            DeliveryRepository deliveryRepository,
            PaymentRepository paymentRepository,
            ProductRepository productRepository,
            OrderAdminService orderAdminService,
            IMapper mapper,
            IFlashMessage flashMessage
        )
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _flashMessage = flashMessage;
            _orderAdminService = orderAdminService;
        }

        private readonly OrderAdminService _orderAdminService;

        public async Task<IActionResult> Index()
        {
            return View(_orderRepository.GetAll());
        }

        private void PopulateViewModel(OrderViewModel vm)
        {
            vm.Deliveries = _deliveryRepository.GetAll().ToList();
            vm.Payments = _paymentRepository.GetAll().ToList();
            vm.Products = _productRepository.GetAll().ToList();
            vm.OrderStatusList = Enum.GetValues<OrderStatus>().ToList();
        }

        private void SetDefaults(OrderViewModel vm, Order order)
        {
            var billingAddress = new OrderViewModel.AddressViewModel();
            var deliveryAddress = new OrderViewModel.AddressViewModel();

            _mapper.Map(order.BillingAddress, billingAddress);
            if (order.DeliveryAddress != null)
            {
                _mapper.Map(order.DeliveryAddress, deliveryAddress);
            }

            vm.Id = order.Id;
            vm.DeliveryAddress = deliveryAddress;
            vm.BillingAddress = billingAddress;
            vm.OrderStatus = order.Status;
            vm.PaymentId = order.Payment.Id;
            vm.DeliveryId = order.Delivery.Id;
            vm.DeliveryTitle = order.DeliveryTitle;
            vm.DeliveryPrice = order.DeliveryPrice;
            vm.PaymentTitle = order.PaymentTitle;
            vm.PaymentPrice = order.PaymentPrice;
            vm.UseDeliveryAddress = order.DeliveryAddress != null;
            vm.Paid = order.Paid;

            foreach (OrderItem item in order.OrderItems)
            {
                vm.Items.Add(new ()
                {
                    Id = item.Id,
                    Price = item.Price,
                    Title = item.Title,
                    Quantity = item.Quantity,
                    ProductId = item.Product?.Id
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }
            
            var vm = new OrderViewModel();
            
            PopulateViewModel(vm);
            SetDefaults(vm, order);
            
            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel vm)
        {
            var order = _orderRepository.GetById(vm.Id);

            if (order == null)
            {
                return NotFound();
            }
            
            PopulateViewModel(vm);

            if (this.Validate(vm) == false)
            {
                return View(vm);
            }

            await _orderAdminService.Map(vm, order);
            await _orderRepository.Save(order);
            
            _flashMessage.Confirmation("Order was edited.");
            
            return RedirectToAction("Edit", new {id = order.Id});
        }
        
        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] OrderViewModel vm)
        {
            PopulateViewModel(vm);
            
            vm.Items.Add(new OrderViewModel.Item());
            
            ModelState.Clear();
            
            return PartialView("Edit", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveItem([FromForm] OrderViewModel vm, [FromForm] int index)
        {
            PopulateViewModel(vm);
            
            vm.Items.RemoveAt(index);
            
            ModelState.Clear();
            
            return PartialView("Edit", vm);
        }
    }
}