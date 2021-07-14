using System;
using System.Linq;
using AutoMapper;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;

namespace DyShop.Services
{
    public class CheckoutService
    {
        private readonly IMapper _mapper;

        public CheckoutService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Order MapOrderFromCart(CartCheckoutViewModel vm)
        {
            var order = new Order();

            var delivery = vm.Deliveries.First(x => x.Id == vm.DeliveryId);
            var payment = vm.Payments.First(x => x.Id == vm.PaymentId);
            
            order.Delivery = delivery;
            order.Payment = payment;
            order.BillingAddress = CreateAddress(vm.BillingAddress);
            order.DeliveryTitle = delivery.Title;
            order.PaymentTitle = payment.Title;
            order.DeliveryPrice = delivery.Price;
            order.PaymentPrice = payment.Price;
            order.TotalPrice = CalculateTotalPrice(vm);

            if (vm.UseDeliveryAddress)
            {
                order.DeliveryAddress = CreateAddress(vm.DeliveryAddress ?? throw new InvalidOperationException());
            }
            
            AddProducts(vm.Cart, order);
            
            return order;
        }

        private void AddProducts(Cart cart, Order order)
        {
            foreach (CartItem item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = item.Product.Price,
                    TotalPrice = item.Price,
                    Quantity = item.Quantity,
                    Title = item.Product.Title,
                    Product = item.Product,
                };
                
                order.OrderItems.Add(orderItem);
            }
        }

        private ClientAddress CreateAddress(CartCheckoutViewModel.AddressViewModel addressViewModel)
        {
            var address = new ClientAddress();
            
            _mapper.Map(addressViewModel, address);

            return address;
        }
        
        public float CalculateTotalPrice(CartCheckoutViewModel vm)
        {
            var price = vm.Cart.TotalPrice();

            var delivery = vm.Deliveries.FirstOrDefault(x => x.Id == vm.DeliveryId);
            var payment = vm.Payments.FirstOrDefault(x => x.Id == vm.PaymentId);

            if (delivery != null)
            {
                price += delivery.Price;
            }
            
            if (payment != null)
            {
                price += payment.Price;
            }

            return price;
        }
    }
}