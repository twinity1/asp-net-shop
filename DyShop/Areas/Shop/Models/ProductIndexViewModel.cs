using System.Collections.Generic;
using DyShop.Data.Entities;
using DyShop.Helpers.Controller;
using DyShop.Helpers.Sorting;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Models
{
    public class ProductIndexViewModel
    {
        [BindProperty] public int Page { get; set; } = 1;
        
        [BindProperty] public List<CheckboxItem>? CategorySelect { get; set; }
        
        [BindProperty] public List<CheckboxItem>? ParameterSelect { get; set; }

        [BindProperty] public Sorting Sort { get; set; } = Sorting.Default;

        [BindProperty] public float PriceFrom { get; set; }
        
        [BindProperty] public float PriceTo { get; set; }
        
        public List<ProductParameterGroup> ParameterGroups { get; set; } = new List<ProductParameterGroup>();
        
        public List<Product> Products { get; set; }
        
        public List<ProductCategory> Categories { get; set; }
        
        public int Count { get; set; }
        
        public int MaxPerPage { get; set; }
        
        public Dictionary<Sorting, string> Sortings { get; set; }
        
        public float PriceRangeFrom { get; set; }
        
        public float PriceRangeTo { get; set; }
    }
}