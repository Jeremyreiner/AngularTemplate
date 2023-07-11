using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Shared.Enums;

namespace Template.Shared.Entities;

[Table("Invoices")]
public class InvoiceEntity
{
    [Key]
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.Now;

    public StatusEnum Status { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Vat { get; set; }
}