using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EShopService0.Models
{
    public class Product
    {
        

        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string name { get; set; } = default;

        public class Category
        {
            public int Id { get; set; }
        }

        [Required]
        public Category category { get; set; }

        public string Ean { get; set; }

        public decimal price { get; set; }

        public int Stock { get; set; } = 0;

        public Guid created_by { get; set; }
    }
}
