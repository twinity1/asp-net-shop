using System.Collections.Generic;
using DyShop.Services.Breadcrumbs;

namespace DyShop.Areas.Shop.Views.Shared.Components.Breadcrumbs
{
    public class BreadcrumbsViewModel
    {
        public ICollection<Breadcrumb> Breadcrumbs { get; set; }
    }
}