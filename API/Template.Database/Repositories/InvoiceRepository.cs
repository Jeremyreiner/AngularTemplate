using System.Linq.Expressions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Template.Database.Infrastructure.MySql;
using Template.Shared.Entities;
using Template.Shared.Interfaces.IRepositories;
using Template.Shared.Results;

namespace Template.Database.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        readonly ApplicationDbContext _DbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Result<InvoiceEntity>> AddAsync(InvoiceEntity post)
        {
            await _DbContext.Invoices.AddAsync(post);

            var count = await _DbContext.SaveChangesAsync();

            return count == 0
                ? Result<InvoiceEntity>.Failed(new Error(HttpStatusCode.NotFound))
                : Result<InvoiceEntity>.Success(post);
        }

        public async Task<Result<InvoiceEntity>> UpdateAsync(InvoiceEntity post)
        {
            _DbContext.Invoices.Update(post);

            var count = await _DbContext.SaveChangesAsync();

            return count == 0
                ? Result<InvoiceEntity>.Failed(new Error(HttpStatusCode.NotFound))
                : Result<InvoiceEntity>.Success(post);
        }

        public async Task<Result<HttpStatusCode>> DeleteAsync(InvoiceEntity post)
        {
            _DbContext.Invoices.Remove(post);

            var count = await _DbContext.SaveChangesAsync();

            return count == 0
                ? Result<HttpStatusCode>.Deleted()
                : Result<HttpStatusCode>.Failed(new Error(HttpStatusCode.NotModified));
        }

        public async Task<Result<InvoiceEntity>> GetByAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate)
        {
            var post = await _DbContext
                .Invoices
                .FirstOrDefaultAsync(predicate);

            return post is not null
                ? Result<InvoiceEntity>.Success(post)
                : Result<InvoiceEntity>
                    .Failed(new Error(HttpStatusCode.NotFound));
        }

        public Task<Result<InvoiceEntity>> GetWithAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public async Task<Result<List<InvoiceEntity>>> GetListWithAsync()
        {
            var posts = await _DbContext.Invoices
                .Include(invoice => invoice.Status)
                .ToListAsync();

            return posts.Any()
                ? Result<List<InvoiceEntity>>.Success(posts)
                : Result<List<InvoiceEntity>>
                    .Failed(new Error(HttpStatusCode.NotFound));
        }

        public async Task<Result<List<InvoiceEntity>>> GetListByAsync()
        {
            var invoices = await _DbContext.Invoices.ToListAsync();

            return invoices.Any()
                ? Result<List<InvoiceEntity>>.Success(invoices)
                : Result<List<InvoiceEntity>>
                    .Failed(new Error(HttpStatusCode.NotFound));
        }
    }
}
