using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? ISBN { get; set; }
        
        [Required]
        public string? Author { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for 1 - 50")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for 51 - 100")]
        public double PricePer50 { get; set; }

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for 100+")]
        public double PricePer100 { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }


        // References to other tables

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; } // We don't need to validate the navigation properties.

        [Required]
        [DisplayName("Cover Type")]
        public int CoverTypeId { get; set; }

        [ValidateNever]
        public CoverType CoverType { get; set; } // We don't need to validate the navigation properties.
    }
}
