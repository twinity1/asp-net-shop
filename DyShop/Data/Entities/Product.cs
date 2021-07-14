using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DyShop.Data.Entities.Types;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Product : ISlugEntity, IBaseEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Slug { get; set; }
        
        public string Description { get; set; }
        
        public float Price { get; set; }
        
        public bool Available { get; set; }
        
        public bool Featured { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        public virtual ProductCategory MainCategory { get; set; }

        public virtual ICollection<ProductCategoryRelation> Categories { get; set; } =
            new List<ProductCategoryRelation>();

        public virtual Product? ProductGroup { get; set; }

        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();
        
        public virtual ICollection<ProductParameterRelation> Parameters { get; set; } = new List<ProductParameterRelation>();

        [NotMapped]
        public ProductPhoto MainPhoto => ProductPhotos.First();

        [NotMapped] 
        public List<ProductPhoto> SecondaryPhotos => ProductPhotos.Skip(1).ToList();
        
        [NotMapped]
        public List<ProductCategory> AllCategories
        {
            get
            {
                var categories = Categories.Select(x => x.ProductCategory).ToList();

                if (categories.Contains(MainCategory) == false)
                {
                    categories.Add(MainCategory);
                }
                
                return categories;
            }
        }
    }
}