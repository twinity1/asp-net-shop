using System.Threading.Tasks;
using DyShop.Services.Breadcrumbs;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Views.Shared.Components.Breadcrumbs
{
    [ViewComponent(Name = "breadcrumbs")]
    public class BreadcrumbsViewComponent : ViewComponent 
    {
        private readonly BreadcrumbsService _breadcrumbsService;

        public BreadcrumbsViewComponent(BreadcrumbsService breadcrumbsService)
        {
            _breadcrumbsService = breadcrumbsService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new BreadcrumbsViewModel
            {
                Breadcrumbs = _breadcrumbsService,
            });
        }
    }
}