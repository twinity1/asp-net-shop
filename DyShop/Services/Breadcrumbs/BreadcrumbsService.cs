using System.Collections.Generic;

namespace DyShop.Services.Breadcrumbs
{
    public class BreadcrumbsService : List<Breadcrumb>
    {
        public BreadcrumbsService()
        {
            Add(new Breadcrumb
            {
                Title = "Domů",
                Controller = "Home",
                Action = "Index",
            });
        }
    }
}