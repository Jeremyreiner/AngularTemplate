using System.Net;
using Template.Shared.Entities;
using Template.Shared.Enums;
using Template.Shared.Models;
using Template.Shared.Results;

namespace Template.Shared.Interfaces.IServices
{
    /// <summary>
    /// This is a template application interface. All values must be updated for proper use cases
    /// This interface allows the initial designer access of CRUD generic functions only.
    /// Interface requires input only for login validation,everything else is autonomously done.
    /// </summary>
    public interface IDalService
    {
        #region Create

        /// <summary>
        /// Creator manager for entities
        /// </summary>
        /// <param name="type">Enum type for entities</param>
        /// <param name="model">Entity property revealed to frontend</param>
        /// <returns></returns>
        Task<Guid> CreateManagerAsync(object model);

        #endregion
        #region Delete

        /// <summary>
        /// Delete methods for entities
        /// </summary>
        /// <returns>HttpStatusResponse</returns>
        Task<HttpStatusCode> DeleteManagerAsync(object model);

        #endregion
        #region Update

        Task<Guid> UpdateManagerAsync(object model);

        #endregion
        #region Get


        /// <summary>
        /// Gets invoice from DB by  GUID id as string
        /// </summary>
        /// <returns></returns>
        Task<Result<InvoiceEntity>> GetInvoiceAsync(string id);

        /// <summary>
        /// Gets all invoices from DB
        /// </summary>
        /// <returns></returns>
        Task<Result<List<InvoiceEntity>>> GetAllInvoices();

        #endregion
    }
}
