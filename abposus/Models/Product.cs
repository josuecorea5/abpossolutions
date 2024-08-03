using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace abposus.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; } = true;
        public List<Sale> Sale { get; set; } = [];
    }
}
