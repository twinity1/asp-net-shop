using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Dasync.Collections;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Helpers.Controller;

namespace DyShop.Services
{
    public class ProductAdminService
    {
        private readonly FileHandlerService _fileHandlerService;
        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly ProductParameterRepository _productParameterRepository;

        public ProductAdminService(
            FileHandlerService fileHandlerService,
            ProductRepository productRepository,
            ProductCategoryRepository productCategoryRepository,
            ProductParameterRepository productParameterRepository
            )
        {
            _fileHandlerService = fileHandlerService;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productParameterRepository = productParameterRepository;
        }

        private async Task SavePhotoFiles(Product product, ProductViewModel viewModel)
        {
            var bag = new ConcurrentBag<ProductPhoto>();

            await viewModel.NewPhotos.ParallelForEachAsync(async file =>
            {
                var fileName = await _fileHandlerService.SaveFile(file, "products");

                bag.Add(new ProductPhoto
                {
                    Product = product,
                    Title = file.Name,
                    SavePath = fileName,
                });
            });

            foreach (var productPhoto in bag)
            {
                product.ProductPhotos.Add(productPhoto);
            }
        }

        private async Task RemovePhotos(Product product, ProductViewModel viewModel)
        {
            var photos = product.ProductPhotos.ToList();

            var i = 0;

            foreach (bool shouldDelete in viewModel.PhotosDelete)
            {
                if (shouldDelete)
                {
                    await _productRepository.RemovePhoto(photos[i], false);
                }

                i++;
            }
        }

        private async Task<Product> SaveProduct(Product product, ProductViewModel viewModel)
        {
            product.Title = viewModel.Title;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Available = viewModel.Available;
            product.Featured = viewModel.Featured;
            product.MainCategory = _productCategoryRepository.GetById(viewModel.ProductMainCategory) ?? throw new InvalidOperationException();

            ManageCategories(product, viewModel);
            ManageParameters(product, viewModel);

            await RemovePhotos(product, viewModel);

            await Task.WhenAll(
                SavePhotoFiles(product, viewModel),
                _productRepository.ChangeGroup(product, viewModel.ProductGroup, false)
            );
            
            await _productRepository.Save(product);
            
            return product;
        }

        private void ManageParameters(Product product, ProductViewModel vm)
        {
            product.Parameters.Clear();

            var ids = vm.SelectedProductParameters.Where(x => x.Checked).Select(x => x.Id);

            var parameters = _productParameterRepository.GetParametersByIds(ids.ToList());

            product.Parameters = parameters.Select(x => new ProductParameterRelation
            {
                Parameter = x,
                Product = product
            }).ToList();
        }

        private void ManageCategories(Product product, ProductViewModel viewModel)
        {
            var selectedCategories = viewModel.SelectedCategories
                .Where(tuple => tuple.Checked)
                .Select(tuple => tuple.Id)
                .ToArray();

            product.Categories.Clear();

            foreach (var productCategory in _productCategoryRepository.GetByIds(selectedCategories))
            {
                product.Categories.Add(new ProductCategoryRelation
                {
                    Product = product,
                    ProductCategory = productCategory,
                });
            }
        }

        public async Task<Product> Insert(ProductViewModel viewModel)
        {
            var product = new Product();

            await SaveProduct(product, viewModel);
            
            return product;
        }

        public async Task<Product> Update(Product product, ProductViewModel viewModel)
        {
            await SaveProduct(product, viewModel);

            return product;
        }
    }
}