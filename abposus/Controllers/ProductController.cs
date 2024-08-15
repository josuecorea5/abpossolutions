using abposus.Interfaces;
using abposus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return View("Error");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var productUpdated = _productRepository.Update(product);

            if(productUpdated)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);

            if(product == null)
            {
                return RedirectToAction("Index");
            }

            var productDeleted = _productRepository.Delete(product);

            if(productDeleted)
            {
                return RedirectToAction("Index");
            }else
            {
                return View("Error");
            }
        }
    }
}
