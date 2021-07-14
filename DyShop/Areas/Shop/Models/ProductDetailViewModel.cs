using System.Collections.Generic;
using System.Linq;
using DyShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        
        public List<Product> ProductGroups { get; set; }

        public List<GroupParameterViewModel> GroupParameters =>
            Product.Parameters.GroupBy(x => x.Parameter.Group)
            .Select(x => new GroupParameterViewModel
            {
                Title = x.Key.Title,
                Parameters = x.Select(p => p.Parameter.Title).ToList(),
            }).ToList();
        
        [BindProperty]
        public int Quantity { get; set; }

        public class GroupParameterViewModel
        {
            public string Title { get; set; }
            
            public List<string> Parameters { get; set; }
        }
    }
}