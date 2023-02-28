using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Cover Type")]
        [Required(ErrorMessage = "Please enter the name.")]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
