using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,5000)]
        public double CustomerPrice { get; set; }
        [Required]
        [Range(1, 5000)]
        public double  ResellerPrice { get; set; }
        [ValidateNever]
        public string PictureUrl { get; set; }
        [Required]
        [ForeignKey("ProductCategoryId")]
        [Display(Name="Product Category")]
        public int ProductCategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory { get; set; }
        [Required]
        [Display(Name = "Product Brand")]
        public int BrandId { get; set; }
        [ValidateNever]
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

    }
}
