﻿using abposus.Data;
using abposus.Interfaces;
using abposus.Models;
using Microsoft.EntityFrameworkCore;

namespace abposus.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private ApplicationDbContext _contex;
        public SaleRepository(ApplicationDbContext context)
        {
            _contex = context;
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _contex.Sales.ToListAsync();
        }
        public void Add(Sale sale)
        {
            _contex.Sales.Add(sale);
        }

        public bool Save()
        {
            var saved = _contex.SaveChanges();
            return saved > 0;
        }
    }
}
