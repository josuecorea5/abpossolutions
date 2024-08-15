using abposus.Interfaces;
using abposus.Models;
using abposus.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel product)
        {

            if (!ModelState.IsValid)
            {

                return View(product);
            }

            var newProduct = new Product()
            {
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity,
            };

            _unitOfWork.ProductRepository.Add(newProduct);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(id);
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

            _unitOfWork.ProductRepository.Update(product);

            var productUpdated = _unitOfWork.Save();

            if(productUpdated)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(id);

            if(product == null)
            {
                return RedirectToAction("Index");
            }

            _unitOfWork.ProductRepository.Delete(product);
            var productDeleted = _unitOfWork.Save();

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
