using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Template.Database.Infrastructure.MySql;
using Template.Shared.Entities;
using Template.Shared.Interfaces.IRepositories;

namespace Template.Database.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        readonly ApplicationDbContext _DbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(InvoiceEntity post)
        {
            await _DbContext.Invoices.AddAsync(post);
            
            await _DbContext.SaveChangesAsync();
        }

        public async Task<Guid> UpdateAsync(InvoiceEntity post)
        {
            _DbContext.Invoices.Update(post);

            var count = await _DbContext.SaveChangesAsync();

            return count == 0
                ? Guid.Empty
                : post.Id;
        }

        public async Task UpdateRangeAsync(List<InvoiceEntity> invoices)
        {
            _DbContext.Invoices.UpdateRange(invoices);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<Guid> DeleteAsync(InvoiceEntity post)
        {
            _DbContext.Invoices.Remove(post);

            var count = await _DbContext.SaveChangesAsync();

            return count == 0
                ? Guid.Empty
                : post.Id;
        }

        public async Task DeleteRangeAsync(List<InvoiceEntity> invoices)
        {
            _DbContext.RemoveRange(invoices);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<InvoiceEntity?> GetByAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate) =>
            await _DbContext
                .Invoices
                .FirstOrDefaultAsync(predicate);


        public async Task<List<InvoiceEntity>> GetListByAsync()
        {
            var invoices = await _DbContext.Invoices.ToListAsync();

            return invoices.Any()
                ? invoices
                : new List<InvoiceEntity>();
        }
    }
}
