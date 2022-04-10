using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Product Brand")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0,150)]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}
