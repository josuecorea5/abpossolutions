using System.ComponentModel.DataAnnotations;

namespace abposus.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Contact {  get; set; } = string.Empty;
        public decimal? TotalPrice { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? PaidDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = [];
    }
}
