using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DyShop.Helpers.Controller
{
    public static class ViewRenderHelper
    {
        public class ViewComponentInfo
        {
            public object Model { get; set; }
            
            public string ComponentName { get; set; }
        } 
        
        public static async Task<string> RenderViewComponent<TModel>(this Microsoft.AspNetCore.Mvc.Controller controller, string componentName, TModel model)
        {
            controller.ViewData.Model = new ViewComponentInfo
            {
                Model = model,
                ComponentName = componentName,
            };

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                var viewResult = viewEngine.GetView("~/Helpers/Controller/_ViewComponent.cshtml", "~/Helpers/Controller/_ViewComponent.cshtml", false);
                
                if (viewResult.Success == false)
                {
                    throw new Exception();
                }

                ViewContext viewContext = new(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}