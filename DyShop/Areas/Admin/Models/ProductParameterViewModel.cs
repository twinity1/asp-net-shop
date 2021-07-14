using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Admin.Models
{
    public class ProductParameterViewModel
    {
        public int Id { get; set; }
        
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        [MinLength(1, ErrorMessage = "Group must have at least one parameter.")]
        public List<ParameterViewModel> Parameters { get; set; } = new List<ParameterViewModel>();

        [BindProperties]
        public class ParameterViewModel
        {
            public int Id { get; set; }
            
            public string Title { get; set; }
        }
    }
}