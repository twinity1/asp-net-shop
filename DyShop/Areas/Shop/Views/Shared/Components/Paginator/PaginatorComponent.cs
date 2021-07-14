using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Views.Shared.Components.Paginator
{
    [ViewComponent(Name = "paginator")]
    public class PaginatorComponent : ViewComponent
    {
        private const int MaxDisplayPageCount = 9;
        
        public async Task<IViewComponentResult> InvokeAsync(int page, int count, int perPage)
        {
            var model = new PaginatorViewModel {CurrentPage = page};

            var pageCount = (int) Math.Ceiling((float) count / perPage);

            var pageCountPrev = MaxDisplayPageCount / 2; 

            for (int i = 0; i < MaxDisplayPageCount; i++)
            {
                var resultPage = page - pageCountPrev + i ;

                if (resultPage > 0 && resultPage <= pageCount)
                {
                    model.Pages.Add(resultPage);

                    if (page - 1 == resultPage)
                    {
                        model.PreviousPage = page - 1;
                    }
                    
                    if (page + 1 == resultPage)
                    {
                        model.NextPage = page + 1;
                    }
                }
            }
            
            return View(model);
        }
    }
}