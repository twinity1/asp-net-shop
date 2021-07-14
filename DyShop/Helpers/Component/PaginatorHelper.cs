using System.Linq;

namespace DyShop.Helpers.Component
{
    public static class PaginatorHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int maxPerPage)
        {
            var skip = maxPerPage * (page - 1);
            
            return query.Skip(skip).Take(maxPerPage);
        }
    }
}