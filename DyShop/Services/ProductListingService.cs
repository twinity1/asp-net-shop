using System.Collections.Generic;
using System.Linq;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Helpers.Component;
using DyShop.Helpers.Sorting;

namespace DyShop.Services
{
    public class ProductListingService
    {
        private readonly ProductParameterRepository _productParameterRepository;

        public ProductListingService(ProductParameterRepository productParameterRepository)
        {
            _productParameterRepository = productParameterRepository;
        }

        public ListingResult Listing(ListingParameters parameters, IQueryable<Product> defaultQuery)
        {
            defaultQuery = defaultQuery.Where(x => x.Available);
            
            var productQuery = GetFilteredQuery(parameters, defaultQuery);
            productQuery = GetSortedQuery(parameters.Sorting, productQuery);
            productQuery = productQuery.Paginate(parameters.Page, parameters.PerPageItems);
            
            return new()
            {
                Products = productQuery.ToList(),
                Count = defaultQuery.Count(),
                MinPrice = defaultQuery.Min(x => x.Price),
                MaxPrice = defaultQuery.Max(x => x.Price),
            };
        }

        private IQueryable<Product> GetFilteredQuery(ListingParameters parameters, IQueryable<Product> query)
        {
            if (parameters.Categories.Count != 0)
            {
                query = query
                    .Where(x => 
                        x.Categories.Any(c => parameters.Categories.Contains(c.ProductCategoryId))
                        ||
                        parameters.Categories.Contains(x.MainCategory.Id)
                        );
            }

            if (parameters.Parameters.Count != 0)
            {
                var groupDic = _productParameterRepository.GetGroupIdsForParameters(parameters.Parameters);
                var parameterGroups = parameters.Parameters.GroupBy(x => groupDic[x]).Select(x => x.ToList());

                foreach (var parameterGroup in parameterGroups)
                {
                    query = query.Where(p => p.Parameters.Any(x => parameterGroup.Contains(x.ParameterId)));
                }
            }

            if (parameters.PriceFrom != 0.0f)
            {
                query = query.Where(x => x.Price >= parameters.PriceFrom);
            }
            
            if (parameters.PriceTo != 0.0f)
            {
                query = query.Where(x => x.Price <= parameters.PriceTo);
            }

            return query;
        }
        
        private IQueryable<Product> GetSortedQuery(Sorting sorting, IQueryable<Product> query)
        {
            switch (sorting)
            {
                case Sorting.CreatedAsc:
                    return query.OrderBy(x => x.CreatedAt);
                case Sorting.CreatedDesc:
                    return query.OrderByDescending(x => x.CreatedAt);
                case Sorting.PriceAsc:
                    return query.OrderBy(x => x.Price);
                case Sorting.PriceDesc:
                    return query.OrderByDescending(x => x.Price);
            }

            return query;
        }
        
        public sealed class ListingParameters
        {
            public Sorting Sorting { get; set; }
            
            public List<int> Categories { get; set; }
            
            public List<int> Parameters { get; set; }

            public float PriceFrom { get; set; }
            
            public float PriceTo { get; set; }
            
            public int Page { get; set; }
            
            public int PerPageItems { get; set; }
        }

        public sealed class ListingResult
        {
            public List<Product> Products { get; set; }
            
            public int Count { get; set; }
            
            public float MinPrice { get; set; }
            
            public float MaxPrice { get; set; }
        }
    }
}