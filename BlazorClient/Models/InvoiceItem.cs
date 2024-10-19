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
        public decimal NetAmount => Total + Taxes.Sum(i => i.Amount);
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<InvoiceItemTax> Taxes { get; set; } = new();
    }
}
