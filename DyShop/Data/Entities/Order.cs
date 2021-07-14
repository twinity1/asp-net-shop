using System;
using System.Collections.Generic;
using DyShop.Data.Entities.Enums;
using DyShop.Data.Entities.Types;

namespace DyShop.Data.Entities
{
    public class Order : IBaseEntity
    {
        public int Id { get; set; }
        
        public string Hash { get; set; } = Guid.NewGuid().ToString();
        
        public bool Paid { get; set; }
        
        public float DeliveryPrice { get; set; }
        
        public float PaymentPrice { get; set; }
        
        public string DeliveryTitle { get; set; }
        
        public string PaymentTitle { get; set; }
        
        public float TotalPrice { get; set; }

        public virtual ClientAddress BillingAddress { get; set; }
        
        public virtual ClientAddress? DeliveryAddress { get; set; }
        
        public virtual Delivery Delivery { get; set; }
        
        public virtual Payment Payment { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public OrderStatus Status { get; set; } = OrderStatus.Created;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}