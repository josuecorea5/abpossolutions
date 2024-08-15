using abposus.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class AccountingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _unitOfWork.SaleRepository.GetAllSales();
            return View(sales);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var sale = await _unitOfWork.SaleRepository.GetById(id);
            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> PaySale(int id)
        {
            var sale = await _unitOfWork.SaleRepository.GetById(id);
            if (sale == null)
            {
                TempData["Error"] = "Sale not found";
                return RedirectToAction("Index");
            }

            await _unitOfWork.SaleRepository.PaySale(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
