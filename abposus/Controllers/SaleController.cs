using abposus.Interfaces;
using abposus.Models;
using abposus.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;

        public SaleController(IProductRepository productRepository, ISaleRepository saleRepository, ISaleProductRepository saleProductRepository)
        {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _saleProductRepository = saleProductRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _saleRepository.GetAllSales();
            return View(sales);
        }

        public async Task<IActionResult> Create()
        {
            var products = await _productRepository.GetAllProducts();
            CreateSaleViewModel viewModel = new CreateSaleViewModel()
            {
                Products = products
            };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Create(CreateSaleViewModel createSaleViewModel)
        {
            try
            {
                var newSale = new Sale()
                {
                    Client = createSaleViewModel.Client,
                    Contact = createSaleViewModel.Contact,
                    Description = createSaleViewModel.Description,
                    TotalPrice = createSaleViewModel.TotalPrice,
                };

                _saleRepository.Add(newSale);
                _saleRepository.Save();

                var saleId = newSale.Id;

                foreach(var product in createSaleViewModel.SaleProducts)
                {
                    var saleProduct = new SaleProduct()
                    {
                        ProductId = product.ProductId,
                        SaleId = saleId,
                    };

                    _saleProductRepository.Add(saleProduct);
                }

                _saleProductRepository.Save();

                return Json(new { message = "Sale created successfully" });

            }
            catch (Exception ex)
            {
                return Json(new { message = "Error to create sale"});
            }
        }
    }
}
