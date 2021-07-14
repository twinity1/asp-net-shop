using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace DyShop.Services.Breadcrumbs
{
    public class Breadcrumb
    {
        public string Title { get; set; }
        
        public string Controller { get; set; }
        
        public string Action { get; set; }

        public dynamic Params { get; set; } = new {};

        public Breadcrumb SetTitle(string title)
        {
            Title = title;
            return this;
        }
        
        public Breadcrumb SetController(string controller)
        {
            Controller = controller;
            return this;
        }
        
        public Breadcrumb SetAction(string action)
        {
            Action = action;
            return this;
        }
        
        public Breadcrumb SetParams(dynamic parameters)
        {
            Params = parameters;
            return this;
        }

        public Dictionary<string, string> ParamsAsDictionary()
        {
            return new RouteValueDictionary(Params).ToDictionary(x => x.Key, x => x.Value.ToString());
        }
    }
}