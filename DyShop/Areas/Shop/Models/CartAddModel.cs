using System;
using System.ComponentModel.DataAnnotations;

namespace DyShop.Areas.Shop.Models
{
    public class CartAddModel
    {
        public string Slug { get; set; }
        
        [Range(1.0f, Double.PositiveInfinity, ErrorMessage = "Chybná hodnota u pole počet kusů.")]
        public float Quantity { get; set; } 
    }
}