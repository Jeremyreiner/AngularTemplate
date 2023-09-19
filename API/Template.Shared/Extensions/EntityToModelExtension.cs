using Template.Shared.Entities;
using Template.Shared.Models;

namespace Template.Shared.Extensions;


public static class EntityToModelExtension
{

    public static InvoiceModel ToModel(this InvoiceEntity entity) =>
        new()
        {
            Id = entity.Id,
            InvoiceNumber = entity.InvoiceNumber,
            Date = entity.Date,
            Status = entity.Status,
        };
}