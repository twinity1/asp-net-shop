using System.Linq;
using System.Threading.Tasks;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Helpers;
using DyShop.Helpers.Collections;
using DyShop.Helpers.Controller;
using DyShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace DyShop.Areas.Admin.Controllers
{
    [Area(Startup.AdminArea)]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductAdminService _productAdminService;
        private readonly IFlashMessage _flashMessage;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly ProductParameterRepository _productParameterRepository;

        public ProductController(
            ProductRepository productRepository,
            ProductAdminService productAdminService,
            IFlashMessage flashMessage,
            ProductCategoryRepository productCategoryRepository,
            ProductParameterRepository productParameterRepository
            )
        {
            _productRepository = productRepository;
            _productAdminService = productAdminService;
            _flashMessage = flashMessage;
            _productCategoryRepository = productCategoryRepository;
            _productParameterRepository = productParameterRepository;
        }

        private void PopulateModel(ProductViewModel vm, Product? product = null)
        {
            vm.ProductGroups = _productRepository.GetProductGroups(product?.Id);
            vm.ProductCategories = _productCategoryRepository.GetAll();
            vm.Product = product;
            vm.ProductParameterGroups = _productParameterRepository.GetAllGroups().ToList();

            if (vm.SelectedCategories.Count == 0)
            {
                vm.SelectedCategories = vm.ProductCategories
                    .Select(category => new CheckboxItem {Checked = false, Id = category.Id})
                    .ToList();
            }

            if (vm.SelectedProductParameters.Count == 0)
            {
                vm.SelectedProductParameters = vm.ProductParameterGroups
                    .SelectMany(g =>
                        g.Parameters.Select(p => new CheckboxItem { Id = p.Id, Checked = false })
                    ).ToList();   
            }
        }

        private void SetDefaults(ProductViewModel vm, Product product)
        {
            vm.Id = product.Id;
            vm.Title = product.Title;
            vm.Description = product.Description;
            vm.Price = product.Price;
            vm.ProductGroup = product.ProductGroup?.Id;
            vm.ProductMainCategory = product.MainCategory.Id;
            vm.Available = product.Available;
            vm.Featured = product.Featured;
            vm.ProductPhotos = product.ProductPhotos.ToList();

            var categories = product.Categories.ToDictionary(x => x.ProductCategoryId, x => true);

            var i = 0;
            
            foreach (var select in vm.SelectedCategories.ToList())
            {
                vm.SelectedCategories[i].Checked = categories.ContainsKey(select.Id);
                i++;
            }

            foreach (var productPhoto in product.ProductPhotos)
            {
                vm.PhotosDelete.Add(false);
            }

            var parameters = product.Parameters.ToDictionary(x => x.ParameterId, x => true);

            foreach (var (item, index) in vm.SelectedProductParameters.WithIndex())
            {
                vm.SelectedProductParameters[index].Checked = parameters.ContainsKey(item.Id);
            }
            
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            
            return View(products);
        }

        [HttpGet]
        public IActionResult New()
        {
            var productFormViewModel = new ProductViewModel();
            
            PopulateModel(productFormViewModel);
            
            return View(productFormViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> New(ProductViewModel productViewModel)
        {
            PopulateModel(productViewModel);
            
            if (this.Validate(productViewModel))
            {
                var product = await _productAdminService.Insert(productViewModel);
                
                _flashMessage.Confirmation("Product was created.");
            
                return RedirectToAction("Edit", product);
            }

            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            
            if (product != null)
            {
                var viewModel = new ProductViewModel();
;                
                PopulateModel(viewModel, product);
                SetDefaults(viewModel, product);
                
                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            var product = _productRepository.GetById(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            PopulateModel(productViewModel, product);

            if (this.Validate(productViewModel))
            {
                await _productAdminService.Update(product, productViewModel);
                
                _flashMessage.Confirmation("Product was updated.");

                return RedirectToAction("Edit", id);
            }

            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteById(id);
            
            _flashMessage.Confirmation("Product was deleted.");
            
            return RedirectToAction("Index");
        }
    }
}