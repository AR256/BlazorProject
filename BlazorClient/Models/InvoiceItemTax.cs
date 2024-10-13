using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorClient.Models
{
    public class InvoiceItemTax
    {
        public Guid Id { get; set; }
        public Guid TaxId { get; set; }
        public decimal Amount { get; set; }
    }
}
