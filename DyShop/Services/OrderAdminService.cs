using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;

namespace DyShop.Services
{
    public class OrderAdminService
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly DeliveryRepository _deliveryRepository;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderAdminService(
            PaymentRepository paymentRepository,
            DeliveryRepository deliveryRepository,
            ProductRepository productRepository,
            IMapper mapper
            )
        {
            _paymentRepository = paymentRepository;
            _deliveryRepository = deliveryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Map(OrderViewModel vm, Order order)
        {
            order.Paid = vm.Paid;
            order.Status = vm.OrderStatus;
            order.Delivery = _deliveryRepository.GetById(vm.DeliveryId) ?? throw new ArgumentException();
            order.DeliveryTitle = vm.DeliveryTitle;
            order.DeliveryPrice = vm.DeliveryPrice;
            order.Payment = _paymentRepository.GetById(vm.PaymentId) ?? throw new ArgumentException();
            order.PaymentTitle = vm.PaymentTitle;
            order.PaymentPrice = vm.PaymentPrice;

            _mapper.Map(vm.BillingAddress, order.BillingAddress);
            
            ManageItems(vm, order);
            ManageDeliveryAddress(vm, order);

            order.TotalPrice = CalculateTotalPrice(vm);
        }

        private void ManageDeliveryAddress(OrderViewModel vm, Order order)
        {
            if (vm.UseDeliveryAddress)
            {
                if (order.DeliveryAddress == null)
                {
                    order.DeliveryAddress = new ClientAddress();
                }

                _mapper.Map(vm.DeliveryAddress, order.DeliveryAddress);
            }
            else
            {
                order.DeliveryAddress = null;
            }
        }

        private void ManageItems(OrderViewModel vm, Order order)
        {
            order.OrderItems.Clear();

            foreach (var item in vm.Items)
            {
                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = item.Price,
                    TotalPrice = item.Price * item.Quantity,
                    Title = item.Title,
                    Quantity = item.Quantity,
                };

                if (item.ProductId != null)
                {
                    orderItem.Product = _productRepository.GetById((int) item.ProductId);
                }
                
                order.OrderItems.Add(orderItem);
            }
            
        }

        private float CalculateTotalPrice(OrderViewModel vm)
        {
            return vm.DeliveryPrice + vm.PaymentPrice + vm.Items.Sum(x => x.Price * x.Quantity);
        }
    }
}