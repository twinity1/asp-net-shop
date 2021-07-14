using DyShop.Services.Breadcrumbs;

namespace DyShop.Helpers.Component
{
    public static class BreadcrumbsHelper
    {
        public static Breadcrumb Breadcrumb(this Microsoft.AspNetCore.Mvc.Controller controller, string title)
        {
            var breadcrumb = new Breadcrumb
            {
                Controller = controller.ControllerContext.ActionDescriptor.ControllerName,
                Action = controller.ControllerContext.ActionDescriptor.ActionName,
                Title = title
            };

            return breadcrumb;
        }
    }
}