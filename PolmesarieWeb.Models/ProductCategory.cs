using System.ComponentModel.DataAnnotations;

namespace PolmesarieWeb.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        //optional description
        [Display(Name = "Category Description")]
        public string? CategoryDescription { get; set; }
        [Display(Name ="Display Order")]
        [Range(1, 250, ErrorMessage ="Display Order must be between 1 and 250")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } =DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        //add user and transaction type


    }
}
