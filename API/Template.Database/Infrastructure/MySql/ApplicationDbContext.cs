using Microsoft.EntityFrameworkCore;
using Template.Shared.Entities;

namespace Template.Database.Infrastructure.MySql
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; } = null!;
        
        public DbSet<InvoiceEntity> Invoices { get; set; } = null!;
    }
}