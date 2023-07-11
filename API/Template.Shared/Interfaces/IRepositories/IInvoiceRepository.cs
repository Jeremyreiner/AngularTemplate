using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Template.Shared.Entities;
using Template.Shared.Results;

namespace Template.Shared.Interfaces.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Result<InvoiceEntity>> AddAsync(InvoiceEntity user);

        Task<Result<InvoiceEntity>> UpdateAsync(InvoiceEntity user);

        Task<Result<HttpStatusCode>> DeleteAsync(InvoiceEntity user);

        Task<Result<InvoiceEntity>> GetByAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate);

        Task<Result<InvoiceEntity>> GetWithAsync(string publicKey, Expression<Func<InvoiceEntity, bool>> predicate);

        Task<Result<List<InvoiceEntity>>> GetListByAsync();

        Task<Result<List<InvoiceEntity>>> GetListWithAsync();
    }
}
