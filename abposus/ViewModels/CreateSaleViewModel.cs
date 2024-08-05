using abposus.Models;

namespace abposus.ViewModels
{
    public class CreateSaleViewModel
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<SaleProduct> SaleProducts { get; set; } = [];
        public string Client {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Contact {  get; set; } = string.Empty;
        public decimal? TotalPrice { get; set; }
    }
}
