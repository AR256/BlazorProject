using BlazorProject.Domain.Entities;

namespace BlazorProject.Application.DTOS
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public InvoiceType Type { get; set; }
        public string Code { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public decimal NetAmount { get; set; }

        public List<InvoiceItemDto> InvoiceItems { get; set; } = [];
    }
}
