using System.Linq;
using DyShop.Data.Entities.Types;
using Microsoft.EntityFrameworkCore;
using Slugify;

namespace DyShop.Services
{
    public class SlugService
    {
        private readonly SlugHelper _slugHelper = new();

        public string Slugify<T>(string str, DbSet<T> dbSet, int? resourceId = null) where T : class, ISlugEntity, IBaseEntity
        {
            var i = 1;

            var slug = "";

            do
            {
                slug = str;

                if (i != 1)
                {
                    slug = $"{slug}-{i}";
                }

                slug = _slugHelper.GenerateSlug(slug);

                i++;
            } while (IsSlugUnique(slug, dbSet, resourceId) == false);
            
            var generateSlug = _slugHelper.GenerateSlug(slug);
            
            
            return generateSlug;
        }

        private bool IsSlugUnique<T>(string slug, DbSet<T> dbSet, int? resourceId = null) where T : class, ISlugEntity, IBaseEntity
        {
            var query = dbSet.Where(x => x.Slug == slug);

            if (resourceId != null)
            {
                query = query.Where(x => x.Id != resourceId);
            }

            return query.Any() == false;
        }
    }
}