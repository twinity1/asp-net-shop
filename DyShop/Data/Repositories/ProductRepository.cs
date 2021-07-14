using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyShop.Data.Entities;
using DyShop.Services;

namespace DyShop.Data.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileHandlerService _fileHandlerService;
        private readonly SlugService _slugService;
        private readonly ProductListingService _productListingService;

        public ProductRepository(
            ApplicationDbContext dbContext,
            FileHandlerService fileHandlerService,
            SlugService slugService,
            ProductListingService productListingService
            )
        {
            _dbContext = dbContext;
            _fileHandlerService = fileHandlerService;
            _slugService = slugService;
            _productListingService = productListingService;
        }

        public ProductListingService.ListingResult Listing(ProductListingService.ListingParameters parameters)
        {
            return _productListingService.Listing(parameters, _dbContext.Products.AsQueryable());
        }
        
        public async Task ChangeGroup(Product product, int? productGroupId, bool commitChanges)
        {
            if (product.ProductGroup?.Id != productGroupId || productGroupId == null)
            {
                if (product.ProductGroup == null)
                {
                    Product? newParent = null;
                    
                    foreach (Product productGroupItem in GetProductGroup(product))
                    {
                        if (newParent == null)
                        {
                            newParent = productGroupItem;
                            productGroupItem.ProductGroup = null;
                        }
                        else
                        {
                            productGroupItem.ProductGroup = newParent;
                        }
                    }
                }
                
                product.ProductGroup = GetById(productGroupId ?? 0);
            }

            if (commitChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
        
        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products.AsQueryable();
        }

        public Product? GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public List<Product> GetProductGroups(int? sourceId)
        {
            var query = _dbContext.Products.Where(product => product.ProductGroup == null);

            if (sourceId != null)
            {
                query = query.Where(product => product.Id != sourceId);
            }
            
            return query.ToList();
        }

        public ICollection<Product> GetProductGroup(Product product)
        {
            var mainProduct = product.ProductGroup ?? product;
            var groupItems = GetProductGroupItems(mainProduct);
            
            groupItems.Add(mainProduct);

            return groupItems;
        }
        
        public ICollection<Product> GetProductGroupAvailable(Product product)
        {
            var mainProduct = product.ProductGroup ?? product;
            var groupItems = GetProductGroupItems(mainProduct);
            
            groupItems.Add(mainProduct);

            return groupItems.Where(x => x.Available).ToList();
        }

        private ICollection<Product> GetProductGroupItems(Product product)
        {
            return _dbContext.Products.Where(x => x.ProductGroup == product).ToList();
        }

        public async Task RemovePhoto(ProductPhoto photo, bool commitChanges)
        {
            _fileHandlerService.RemoveFile(photo.SavePath);
            _dbContext.ProductPhotos.Remove(photo);

            if (commitChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                var photos = product.ProductPhotos.ToList();

                foreach (var photo in photos)
                {
                    await RemovePhoto(photo, false);
                }

                await ChangeGroup(product, null, false);
                
                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync();                
            }
        }

        public async Task Save(Product product)
        {
            product.Slug = _slugService.Slugify(product.Title, _dbContext.Products, product.Id);
            
            if (product.Id == 0)
            {
                _dbContext.Products.Add(product);

                foreach (var photo in product.ProductPhotos)
                {
                    _dbContext.ProductPhotos.Add(photo);
                }
            }
            
            product.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetFeatured()
        {
            return _dbContext.Products.Where(product => product.Featured).AsQueryable();
        }

        public Product? GetBySlug(string slug, bool onlyAvailable = true)
        {
            var query = _dbContext.Products.AsQueryable();

            if (onlyAvailable)
            {
                query = query.Where(x => x.Available);
            }
            
            return query.FirstOrDefault(x => x.Slug == slug);
        }
    }
}