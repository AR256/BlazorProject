using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorClient.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public InvoiceType Type { get; set; }
        public string Code { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public decimal NetAmount { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new();

    }
    public enum InvoiceType
    {
        Invoice,
        Debt,
        Credit
    };


}
