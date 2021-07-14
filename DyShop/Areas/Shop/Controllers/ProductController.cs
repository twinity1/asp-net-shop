using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Repositories;
using DyShop.Helpers.Component;
using DyShop.Helpers.Controller;
using DyShop.Helpers.Sorting;
using DyShop.Services.Breadcrumbs;
using DyShop.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace DyShop.Areas.Shop.Controllers
{
    [Area(Startup.ShopArea)]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly BreadcrumbsService _breadcrumbsService;
        private readonly ProductParameterRepository _productParameterRepository;
        private readonly MailService _mailService;

        private const int MaxPerProductsPage = 6;

        private const string IndexTitle = "Produkty"; 

        public ProductController(
            ProductRepository productRepository,
            ProductCategoryRepository productCategoryRepository,
            BreadcrumbsService breadcrumbsService,
            ProductParameterRepository productParameterRepository,
            MailService mailService
            )
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _breadcrumbsService = breadcrumbsService;
            _productParameterRepository = productParameterRepository;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductIndexViewModel viewModel)
        {
            _breadcrumbsService.Add(this.Breadcrumb(IndexTitle));
            
            var listing = _productRepository.Listing(new()
            {
                Categories = viewModel.CategorySelect?
                    .Where(x => x.Checked)
                    .Select(x => x.Id)
                    .ToList() ?? new List<int>(),
                Parameters = viewModel.ParameterSelect?
                    .Where(x => x.Checked)
                    .Select(x => x.Id)
                    .ToList() ?? new List<int>(),
                Page = viewModel.Page,
                PriceFrom = viewModel.PriceFrom,
                PriceTo = viewModel.PriceTo,
                PerPageItems = MaxPerProductsPage,
                Sorting = viewModel.Sort,
            });

            viewModel.Products = listing.Products;
            viewModel.PriceRangeFrom = listing.MinPrice;
            viewModel.PriceRangeTo = listing.MaxPrice;
            viewModel.Count = listing.Count;
            
            viewModel.MaxPerPage = MaxPerProductsPage;
            viewModel.Categories = _productCategoryRepository.GetAll();
            viewModel.ParameterGroups = _productParameterRepository.GetAllGroups().ToList();
            viewModel.Sortings = GetSortings();

            if (viewModel.PriceFrom < viewModel.PriceRangeFrom)
            {
                viewModel.PriceFrom = viewModel.PriceRangeFrom;
            }
            
            if (viewModel.PriceTo > viewModel.PriceRangeTo || viewModel.PriceTo == 0)
            {
                viewModel.PriceTo = viewModel.PriceRangeTo;
            }
            
            return View(viewModel);
        }

        public async Task<IActionResult> Category(string category)
        {
            return View("Index");
        }

        private Dictionary<Sorting, string> GetSortings()
        {
            return new()
            {
                {Sorting.Default, "Výchozí řazení"},
                {Sorting.PriceAsc, "Cena vzestupně"},
                {Sorting.PriceDesc, "Cena sestupně"},
                {Sorting.CreatedDesc, "Nejnovější"},
                {Sorting.CreatedAsc, "Nejstarší"},
            };
        }
        
        public async Task<IActionResult> Detail(string slug)
        {
            var product = _productRepository.GetBySlug(slug);
            
            if (product == null)
            {
                return NotFound();
            }
            
            _breadcrumbsService.Add(this.Breadcrumb(IndexTitle).SetAction("Index"));
            _breadcrumbsService.Add(this.Breadcrumb(product.MainCategory.Title).SetAction("Category").SetParams(new { Category = product.MainCategory.Id }));
            _breadcrumbsService.Add(this.Breadcrumb($"{product.Title}"));
            
            var model = new ProductDetailViewModel
            {
                Product = product,
                ProductGroups = _productRepository.GetProductGroupAvailable(product).ToList(),
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Enquiry(ProductEnquiryModel model)
        {
            if (this.Validate(model) == false)
            {
                return PartialView("_Enquiry", model);
            }

            await _mailService.Send(new MailMessage<ProductEnquiryModel>
            {
                FillDefaultEmailTo = true,
                Model = model,
                Subject = "Nový dotaz ohledně produktu.",
                TemplateName = "Product/Enquiry"
            });

            return PartialView("_EnquirySuccess");
        }
    }
}