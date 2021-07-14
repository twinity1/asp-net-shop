using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DyShop.Data.Entities;
using DyShop.Data.Entities.Enums;
using DyShop.Helpers.Validation;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        
        [BindProperty]
        public AddressViewModel BillingAddress { get; set; }

        [BindProperty]
        [ValidateIf(nameof(UseDeliveryAddress))]
        public AddressViewModel? DeliveryAddress { get; set; }
        
        [BindProperty]
        public bool UseDeliveryAddress { get; set; }

        [BindProperty]
        [Required]
        public int DeliveryId { get; set; }

        [BindProperty]
        [Required] 
        public string DeliveryTitle { get; set; } = "";
        
        [BindProperty]
        [Required]
        public float DeliveryPrice { get; set; }

        [BindProperty]
        [Required]
        public int PaymentId { get; set; }

        [BindProperty]
        [Required]
        public string PaymentTitle { get; set; } = "";
        
        [BindProperty]
        [Required]
        public float PaymentPrice { get; set; }
        
        [BindProperty]
        public List<Item> Items { get; set; } = new List<Item>();

        [BindProperty]
        public float TotalPrice { get; set; }
        
        [BindProperty]
        public OrderStatus OrderStatus { get; set; }
        
        [BindProperty]
        public bool Paid { get; set; }
        
        public List<OrderStatus> OrderStatusList { get; set; } = new List<OrderStatus>();
        
        public List<Delivery> Deliveries { get; set; } = new List<Delivery>();

        public List<Payment> Payments { get; set; } = new List<Payment>();

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperties]
        public class Item
        {
            public int Id { get; set; }
            
            public float Price { get; set; }

            [Range(1, Double.PositiveInfinity)]
            public float Quantity { get; set; } = 1;
            
            public int? ProductId { get; set; }
            
            public string Title { get; set; }
        }
        
        [BindProperties]
        public class AddressViewModel
        {
            [Required]
            public string Name { get; set; }
        
            [Phone]
            [Required]
            public string Phone { get; set; }
        
            [EmailAddress]
            [Required]
            public string Email { get; set; }
        
            [Required]
            public string City { get; set; }
        
            [Required]
            public string Street { get; set; }
        
            [Required]
            public string ZipCode { get; set; }    
            
            public string? Note { get; set; }
        }
    }
}