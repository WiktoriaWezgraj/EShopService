using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models
{
    [Table("Products")]
    public class Product : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)] // Standard EAN-13
        public string Ean { get; set; } = string.Empty;

        [Required]
        //cos jeszcze
        public decimal Price { get; set; }

        public int Stock { get; set; } = 0;

        public string Sku { get; set; } = string.Empty;

        public Category Category { get; set; } = default!;
    }
}
