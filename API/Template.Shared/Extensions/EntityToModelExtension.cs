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


    public static UserModel ToModel(this UserEntity entity) =>
        new()
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
        };


    public static List<UserModel> ToModelList(this IEnumerable<UserEntity> list) =>
        list
            .Select(entity => entity
                .ToModel())
            .ToList();
}