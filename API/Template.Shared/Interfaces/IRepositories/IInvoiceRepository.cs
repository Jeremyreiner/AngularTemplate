using System.Linq.Expressions;
using Template.Shared.Entities;

namespace Template.Shared.Interfaces.IRepositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(InvoiceEntity invoice);

        Task<Guid> UpdateAsync(InvoiceEntity invoice);

        Task UpdateRangeAsync(List<InvoiceEntity> invoices);

        Task<Guid> DeleteAsync(InvoiceEntity invoice);

        Task DeleteRangeAsync(List<InvoiceEntity> invoice);

        Task<InvoiceEntity?> GetByAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate);

        Task<List<InvoiceEntity>> GetListByAsync();
    }
}
