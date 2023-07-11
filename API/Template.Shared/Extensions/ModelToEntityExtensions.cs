using Template.Shared.Models;
using Template.Shared.Entities;

namespace Template.Shared.Extensions;


public static class ModelToEntityExtensions
{
    public static UserEntity ToEntity(this UserModel model) =>
        new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
        };

    public static InvoiceEntity ToEntity(this InvoiceModel model) =>
        new()
        {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            Date = model.Date,
            Status = model.Status,
            Vat = model.Vat,
            TotalAmount = model.TotalAmount
        };


    private static Guid ValidateGuid(string key)
    {
        var valid = Guid.TryParse(key, out var guid);

        return valid 
            ? guid
            : Guid.Empty;
    }
}