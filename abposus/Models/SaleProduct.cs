using System.ComponentModel.DataAnnotations;

namespace abposus.Models
{
    public class SaleProduct
    {
        [Key]
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
    }
}
