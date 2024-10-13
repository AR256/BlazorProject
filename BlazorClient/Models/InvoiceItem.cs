using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorClient.Models
{
    public class InvoiceItem
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Total => Quantity * Amount;
        public decimal NetAmount { get; set; }
        public List<InvoiceItemTax> Taxes { get; set; } = new();
    }
}
