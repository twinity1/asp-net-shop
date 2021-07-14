using System;

namespace DyShop.Helpers.Controller
{
    public static class CartCookieHelper
    {
        public const string CookieName = "_cartId";

        public static string CartHashCookieValue(this Microsoft.AspNetCore.Mvc.Controller controller)
        {
            var cookie = controller.Request.Cookies[CookieName];

            var guid = Guid.NewGuid();
 
            if (cookie != null && cookie.Length != guid.ToString().Length)
            {
                cookie = null;
            }
            
            if (cookie == null)
            {
                cookie = guid.ToString();
                controller.Response.Cookies.Append(CookieName, cookie);
            }
            
            return cookie;
        } 
    }
}