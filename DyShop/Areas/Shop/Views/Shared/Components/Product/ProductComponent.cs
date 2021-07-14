using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Views.Shared.Components.Product
{
    [ViewComponent(Name = "product")]
    public class ProductComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Data.Entities.Product model)
        {
            return View(model);
        }
    }
}