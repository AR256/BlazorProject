
using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Models
{
    public class Tax
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Code must be alphanumeric and in uppercase.")]
        public string Code { get; set; }
    }
}
