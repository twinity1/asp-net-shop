using System.Threading.Tasks;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;

namespace DyShop.Services
{
    public class ProductCategoryAdminService
    {
        private readonly ProductCategoryRepository _productCategoryRepository;

        public ProductCategoryAdminService(ProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategory> Save(ProductCategoryViewModel viewModel)
        {
            var category = new ProductCategory();
            
            if (viewModel.Id != 0)
            {
                category = _productCategoryRepository.GetById(viewModel.Id);
            }

            category.Title = viewModel.Title;

            await _productCategoryRepository.Save(category);

            return category;
        }
    }
}