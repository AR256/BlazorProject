using BlazorClient.Pages.Items;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorClient.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "Invoice type is required.")]
        public Guid InvoiceTypeId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public DateTime ModificationDate { get; set; }
        public decimal NetAmount => InvoiceItems.Sum(x => x.NetAmount);
        public List<InvoiceItem> InvoiceItems { get; set; } = new();
    }


}
