using abposus.Interfaces;
using abposus.Models;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {

                return View(product);
            }

            _productRepository.Add(product);

            return RedirectToAction("Index");
        }
    }
}
