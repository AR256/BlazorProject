using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Domain.Entities
{
    public class InvoiceItemTax
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(InvoiceItem))]
        [Required]
        public Guid InvoiceItemId { get; set; }
        [ForeignKey(nameof(Tax))]
        [Required]
        public Guid TaxId { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        public InvoiceItem InvoiceItem { get; set; }

        public Tax Tax { get; set; }
    }
}
