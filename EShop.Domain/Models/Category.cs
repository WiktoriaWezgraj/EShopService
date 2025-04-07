using EShop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models;

    //[Table("Categories")]
    public class Category : BaseModel
    {
        //[Required]
        //[MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }


