using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DyShop.Helpers.Html
{
    public static class ViewDataHelper
    {
        public static void HideBreadcrumbs(this ViewDataDictionary viewDataDictionary)
        {
            viewDataDictionary["HideBreadcrumbs"] = true;
        }
        
        public static void HideHeading(this ViewDataDictionary viewDataDictionary)
        {
            viewDataDictionary["HideHeading"] = true;
        }
    }
}