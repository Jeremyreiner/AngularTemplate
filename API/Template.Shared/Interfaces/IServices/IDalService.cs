using System.Net;
using Template.Shared.Entities;
using Template.Shared.Models;

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
        /// <param name="model">Entity property revealed to frontend</param>
        /// <returns></returns>
        Task<Guid> CreateManagerAsync(InvoiceModel model);

        #endregion
        #region Delete

        /// <summary>
        /// Delete methods for entities
        /// </summary>
        /// <returns>HttpStatusResponse</returns>
        Task<Guid> DeleteManagerAsync(InvoiceModel model);

        #endregion
        #region Update

        Task<Guid> UpdateManagerAsync(InvoiceModel model);

        #endregion
        #region Get

        /// <summary>
        /// Gets invoice from DB by  GUID id as string
        /// </summary>
        /// <returns></returns>
        Task<InvoiceEntity?> GetInvoiceAsync(string id);

        /// <summary>
        /// Gets all invoices from DB
        /// </summary>
        /// <returns></returns>
        Task<List<InvoiceEntity>> GetAllInvoices();

        #endregion
        #region

        Task InvoiceTimeEventManagerAsync(CancellationToken ct);

        #endregion

    }
}
