using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Domain.Entities
{
    public class InvoiceItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey(nameof(Item))]
        public Guid ItemId { get; set; }
        [Required]
        [ForeignKey(nameof(Invoice))]
        public Guid InvoiceId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Item Item { get; set; }
        public Invoice Invoice { get; set; }
        public ICollection<InvoiceItemTax> InvoiceItemTaxes { get; set; } = new HashSet<InvoiceItemTax>();
    }
}
