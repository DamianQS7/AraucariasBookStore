using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AraucariasBookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        // We are using this attribute because we did not define the label in the form, so it was taking the name directly
        // from the name of this property DisplayOrder
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100.")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
