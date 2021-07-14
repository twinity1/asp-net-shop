using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Helpers;
using DyShop.Helpers.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DyShop.Areas.Admin.Models
{
    public class ProductViewModel : IValidatableObject
    {
        [BindProperty]
        public int Id { get; set; }
        
        [BindProperty]
        [Required]
        public string Title { get; set; }
        
        [BindProperty]
        [Required]
        public string Description { get; set; }
        
        [BindProperty]
        [Required]
        public float Price { get; set; }
        
        [BindProperty]
        public bool Available { get; set; }
        
        [BindProperty]
        public bool Featured { get; set; }

        [BindProperty]
        public List<IFormFile>? NewPhotos { get; set; } = new List<IFormFile>();

        [BindProperty]
        public List<bool> PhotosDelete { get; set; } = new List<bool>();

        [BindProperty]
        public int? ProductGroup { get; set; }
        
        [BindProperty]
        public int ProductMainCategory { get; set; }

        [BindProperty]
        public List<CheckboxItem> SelectedCategories { get; set; } = new List<CheckboxItem>();

        [BindProperty]
        public List<CheckboxItem> SelectedProductParameters { get; set; } = new List<CheckboxItem>();

        public Product? Product { get; set; }

        public List<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        
        public List<Product> ProductGroups { get; set; } = new List<Product>();

        public List<ProductParameterGroup> ProductParameterGroups { get; set; } = new List<ProductParameterGroup>();
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var productRepo = (ProductRepository) validationContext.GetService(typeof(ProductRepository))!;
            var product = productRepo.GetById(Id);
            
            if ((product?.ProductPhotos.Count ?? 0) + (NewPhotos?.Count ?? 0) - PhotosDelete.Count(b => b) <= 0)
            {
                yield return new ValidationResult("Product must contain at least one photo.", new []{nameof(NewPhotos)});
            }   
        }
    }
}