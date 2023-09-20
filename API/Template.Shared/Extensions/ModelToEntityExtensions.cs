using Template.Shared.Models;
using Template.Shared.Entities;

namespace Template.Shared.Extensions;

public static class ModelToEntityExtensions
{
    public static UserEntity ToEntity(this UserModel model) =>
        new()
        {
            PublicId = model.Id.ValidGuid(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Bio = model.Bio,
            Email = model.Email,
            CreatedOnDt = model.CreatedOnDt,
            LastUpdateOnDt = model.LastUpdateOnDt,
        };

    public static PostEntity ToEntity(this PostModel model) =>
        new()
        {
            PublicId = model.PublicId.ValidGuid(),
            UserPublicId = model.UserPublicId.ValidGuid(),
            Description = model.Description,
            CreatedOnDt = model.CreatedOnDt,
        };
        
    public static Guid ValidGuid(this string key)
    {
        var valid = Guid.TryParse(key, out var guid);

        return valid
            ? guid
            : Guid.Empty;
    }
}