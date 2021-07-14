using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DyShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Admin.Models
{
    [BindProperties]
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public static ProductCategoryViewModel Create(ProductCategory productCategory)
        {
            return new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Title = productCategory.Title,
            };
        }
    }
}