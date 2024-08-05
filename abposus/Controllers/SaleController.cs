using abposus.Interfaces;
using abposus.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class SaleController : Controller
    {
        private readonly IProductRepository _productRepository;

        public SaleController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
