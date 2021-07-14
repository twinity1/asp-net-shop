using System.Threading.Tasks;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Repositories;
using DyShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace DyShop.Areas.Admin.Controllers
{
    [Area(Startup.AdminArea)]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryAdminService _productCategoryAdminService;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly IFlashMessage _flashMessage;

        public ProductCategoryController(ProductCategoryAdminService productCategoryAdminService, ProductCategoryRepository productCategoryRepository, IFlashMessage flashMessage)
        {
            _productCategoryAdminService = productCategoryAdminService;
            _productCategoryRepository = productCategoryRepository;
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View(_productCategoryRepository.GetAll());
        }

        [HttpGet]
        public IActionResult New()
        {
            return View(new ProductCategoryViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> New(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = await _productCategoryAdminService.Save(model);
                
                _flashMessage.Confirmation("Category was created.");
                
                return RedirectToAction("Edit", new { entity.Id });
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _productCategoryRepository.GetById(id);
            
            if (entity == null)
            {
                return NotFound();
            }
            
            return View(ProductCategoryViewModel.Create(entity));
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = await _productCategoryAdminService.Save(model);
                
                _flashMessage.Confirmation("Category was updated.");
                
                return RedirectToAction("Edit", new { entity.Id });
            }
            
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productCategoryRepository.Delete(id);
            
            _flashMessage.Confirmation("Category was deleted.");
            
            return RedirectToAction("Index");
        }
    }
}