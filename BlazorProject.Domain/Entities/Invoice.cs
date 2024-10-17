using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [ForeignKey(nameof(Customer))]
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid InvoiceTypeId { get; set; }
        [Required]
        public DateTime DateTimeIssued { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();
        public Customer Customer { get; set; }
        public InvoiceType NInvoiceType { get; set; }
    }

}
