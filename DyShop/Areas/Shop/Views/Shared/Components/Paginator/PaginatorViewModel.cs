using System.Collections.Generic;

namespace DyShop.Areas.Shop.Views.Shared.Components.Paginator
{
    public class PaginatorViewModel
    {
        public int? PreviousPage { get; set; }
        
        public int? NextPage { get; set; }
        
        public int CurrentPage { get; set; }

        public List<int> Pages { get; set; } = new List<int>();
    }
}