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
        Task<Guid> CreateManagerAsync(ClassType classType, object model);

        #endregion
        #region Delete

        /// <summary>
        /// Delete methods for entities
        /// </summary>
        /// <returns>HttpStatusResponse</returns>
        Task<HttpStatusCode> DeleteManagerAsync(ClassType classType, object model);

        #endregion
        #region Update

        Task<Guid> UpdateManagerAsync(ClassType classType, object model);

        #endregion
        #region Get

        /// <summary>
        /// Gets user from DB by id.
        /// </summary>
        /// <returns>Result of type User</returns>
        Task<Result<UserEntity>> GetUserAsync(string id);


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

        #region Athentication

        /// <summary>
        /// Login requires a query to the db using an email. if an entity is registered,
        /// then if the entered password matches the entities hashed password, than a succesful entity is returned
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Result of type User</returns>
        Task<Result<UserEntity>> Login(string email, string password);

        /// <summary>
        /// Verifies that the entered New password, and confirmed new password match,
        /// Verifies that the original password, is indeed the original password,
        /// if both these checks pass, then the new password is updated and the user is saved
        /// </summary>
        /// <param name="email"></param>
        /// <param name="model"></param>
        /// <returns>Result of type User</returns>
        Task<Result<UserEntity>> ChangePassword(ChangePasswordModel model);

        #endregion
        #region

        void CheckForThrow(Error error);
        
        #endregion

    }
}
