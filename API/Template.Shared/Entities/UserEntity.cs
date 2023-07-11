using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Shared.Entities;


[Table("Users")]
public class UserEntity
{
    public UserEntity()
    {
        Invoices = new HashSet<InvoiceEntity>();

    }

    [Key]
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public ICollection<InvoiceEntity> Invoices { get; set; }

    [NotMapped] 
    public string FullName => $"{FirstName} {LastName}";
}