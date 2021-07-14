using System.Linq;
using System.Threading.Tasks;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Helpers.Controller;
using DyShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace DyShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(Startup.AdminArea)]
    public class ProductParameterController : Controller
    {
        private readonly ProductParameterRepository _productParameterRepository;
        private readonly ProductParameterAdminService _productParameterAdminService;
        private readonly IFlashMessage _flashMessage;

        public ProductParameterController(
            ProductParameterRepository productParameterRepository,
            ProductParameterAdminService productParameterAdminService,
            IFlashMessage flashMessage
        )
        {
            _productParameterRepository = productParameterRepository;
            _productParameterAdminService = productParameterAdminService;
            _flashMessage = flashMessage;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(_productParameterRepository.GetAllGroups());
        }

        private void PopulateModel(ProductParameterViewModel vm)
        {
            
        }

        private void SetDefaults(ProductParameterViewModel vm, ProductParameterGroup parameterGroup)
        {
            vm.Id = parameterGroup.Id;
            vm.Title = parameterGroup.Title;
            vm.Parameters = parameterGroup.Parameters.Select(x => new ProductParameterViewModel.ParameterViewModel()
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var vm = new ProductParameterViewModel();
            
            PopulateModel(vm);
            
            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> New(ProductParameterViewModel vm)
        {
            PopulateModel(vm);

            if (this.Validate(vm) == false)
            {
                return View(vm);
            } 

            var productParameterGroup = new ProductParameterGroup();
            _productParameterAdminService.Map(vm, productParameterGroup);

            await _productParameterRepository.Save(productParameterGroup);
            
            _flashMessage.Confirmation("Parameter group was created.");
            
            return RedirectToAction("Edit", new {Id = productParameterGroup.Id});
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new ProductParameterViewModel();

            var group = _productParameterRepository.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }
            
            PopulateModel(vm);
            SetDefaults(vm, group);
            
            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ProductParameterViewModel vm)
        {
            PopulateModel(vm);
            
            var group = _productParameterRepository.GetGroupById(vm.Id);

            if (group == null)
            {
                return NotFound();
            }

            if (this.Validate(vm) == false)
            {
                return View(vm);
            } 

            _productParameterAdminService.Map(vm, group);

            await _productParameterRepository.Save(group);
            
            _flashMessage.Confirmation("Parameter group was edited.");
            
            return RedirectToAction("Edit", new {Id = group.Id});
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] ProductParameterViewModel vm)
        {
            PopulateModel(vm);

            vm.Parameters.Add(new ProductParameterViewModel.ParameterViewModel());
            
            ModelState.Clear();

            return PartialView("_Form", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveItem([FromForm] ProductParameterViewModel vm, int index)
        {
            PopulateModel(vm);
            
            vm.Parameters.RemoveAt(index);
            
            ModelState.Clear();

            return PartialView("_Form", vm);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var group = _productParameterRepository.GetGroupById(id);
            
            if (group == null)
            {
                _flashMessage.Danger("Group doest not exist - nothing was deleted.");
            }
            else
            {
                await _productParameterRepository.Delete(group);
                _flashMessage.Confirmation("Group was deleted.");
            }
            
            return RedirectToAction("Index");
        }
    }
}