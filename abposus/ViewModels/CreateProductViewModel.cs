using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abposus.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
