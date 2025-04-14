using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models;

    public class Category : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }


