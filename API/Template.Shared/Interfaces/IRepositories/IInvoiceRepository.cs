using System.Linq.Expressions;
using System.Net;
using Template.Shared.Entities;
using Template.Shared.Results;

namespace Template.Shared.Interfaces.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Result<InvoiceEntity>> AddAsync(InvoiceEntity invoice);

        Task<Result<InvoiceEntity>> UpdateAsync(InvoiceEntity invoice);

        Task UpdateRangeAsync(List<InvoiceEntity> invoices);

        Task<Result<HttpStatusCode>> DeleteAsync(InvoiceEntity invoice);

        Task<Result<HttpStatusCode>> DeleteRangeAsync(List<InvoiceEntity> invoice);

        Task<Result<InvoiceEntity>> GetByAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate);

        Task<Result<List<InvoiceEntity>>> GetListByAsync();
    }
}
