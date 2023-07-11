using Template.Shared.Enums;

namespace Template.Shared.Models;

public class InvoiceModel
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.Now;

    public StatusEnum Status { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Vat { get; set; }
}