using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models;

//[Table("Products")]- nie trzeba
public class Product : BaseModel
{
    //[Key]- nie trzeba
    public int Id { get; set; }

    [Required] 
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty; //?

    [Required]
    [MaxLength(13)] // Standard EAN-13
    public string Ean { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;

    public string Sku { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = default!;
}
