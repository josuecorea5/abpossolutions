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
        private readonly IUnitOfWork _unitOfWork;

        public SaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _unitOfWork.SaleRepository.GetAllSales();
            return View(sales);
        }

        public async Task<IActionResult> Create()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProducts();
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

                _unitOfWork.SaleRepository.Add(newSale);
                _unitOfWork.Save();

                var saleId = newSale.Id;

                foreach(var product in createSaleViewModel.SaleProducts)
                {
                    var saleProduct = new SaleProduct()
                    {
                        ProductId = product.ProductId,
                        SaleId = saleId,
                    };

                    _unitOfWork.SaleProductRepository.Add(saleProduct);
                }

                _unitOfWork.Save();

                return Json(new { message = "Sale created successfully" });

            }
            catch (Exception ex)
            {
                return Json(new { message = "Error to create sale"});
            }
        }
    }
}
