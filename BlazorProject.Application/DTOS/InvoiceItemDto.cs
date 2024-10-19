namespace BlazorProject.Application.DTOS
{
    public class InvoiceItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid InvoiceId { get; set; } 
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Total => Quantity * Amount; 
        public decimal NetAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public List<InvoiceItemTaxDto> Taxes { get; set; } = []; 
    }
}
