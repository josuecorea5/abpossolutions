﻿using abposus.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class AccountingController : Controller
    {
        private readonly ISaleRepository _saleRepository;

        public AccountingController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _saleRepository.GetAllSales();
            return View(sales);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var sale = await _saleRepository.GetById(id);
            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> PaySale(int id)
        {
            var sale = await _saleRepository.GetById(id);

            if(sale == null)
            {
                TempData["Error"] = "Sale not found";
                return RedirectToAction("Index");
            }

            await _saleRepository.PaySale(id);

            return RedirectToAction("Index");
        }
    }
}
