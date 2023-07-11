using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Shared.Entities;
using Template.Shared.Enums;
using Template.Shared.Exceptions;
using Template.Shared.Extensions;
using Template.Shared.Interfaces.IRepositories;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;
using Template.Shared.Results;

namespace Template.Shared.Services
{
    public class DalService : IDalService
    {
        readonly IUserRepository _UserRepository;

        private readonly IInvoiceRepository _InvoiceRepository;

        readonly ILogger<DalService> _Logger;

        public DalService(
            IUserRepository userRepository,
            ILogger<DalService> logger, IInvoiceRepository invoiceRepository)
        {
            _UserRepository = userRepository;
            _Logger = logger;
            _InvoiceRepository = invoiceRepository;
        }


        // CREATE
        public async Task<Guid> CreateManagerAsync(ClassType classType, object model)
        {
            switch (classType)
            {
                case ClassType.User:
                    var user = (UserModel)model;

                    return await CreateUserAsync(user);
                case ClassType.Invoice:
                    var invoice = (InvoiceModel)model;

                    return await CreateInvoiceAsync(invoice);
                default:
                    return Guid.Empty;
            }
        }


        // DELETE
        public async Task<HttpStatusCode> DeleteManagerAsync(ClassType classType, object model)
        {
            switch (classType)
            {
                case ClassType.User:
                    var user = (UserModel)model;

                    var response = await DeleteUserAsync(user.Id);

                    return response.Status;
                case ClassType.Invoice:
                    var invoice = (InvoiceModel)model;

                    var result = await DeleteInvoiceAsync(invoice.Id.ToString());

                    return result.Status;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }

        // UPDATE
        public async Task<Guid> UpdateManagerAsync(ClassType classType, object model)
        {
            switch (classType)
            {
                case ClassType.User:
                    var user = (UserModel)model;

                    var updatedUser = user.ToEntity();
                    
                    var updated = await _UserRepository.UpdateAsync(updatedUser);
                    
                    return updated.Value.Id;
                case ClassType.Invoice:
                    var invoice = (InvoiceModel)model;

                    var response = await _InvoiceRepository.UpdateAsync(invoice.ToEntity());
                    
                    return response.Value.Id;
                default:
                    return Guid.Empty;
            }
        }

        // GET
        public async Task<Result<UserEntity>> GetUserAsync(string id)
        {
            var guid = ValidateGuid(id);

            if (guid != Guid.Empty)
                return await _UserRepository.GetByAsync(u => u.Id == guid);

            return Result<UserEntity>.Failed(new Error(HttpStatusCode.UnprocessableEntity));
        }

        public async Task<Result<InvoiceEntity>> GetInvoiceAsync(string id)
        {
            var guid = ValidateGuid(id);

            if (guid != Guid.Empty)
                return await _InvoiceRepository.GetByAsync(id, u => u.Id == guid);
            
            return Result<InvoiceEntity>.Failed(new Error(HttpStatusCode.UnprocessableEntity));
        }

        public async Task<Result<List<InvoiceEntity>>> GetAllInvoices() => await _InvoiceRepository.GetListByAsync();


        // AUTH
        public async Task<Result<UserEntity>> Login(string email, string password)
        {
            var result = await _UserRepository.GetByAsync(u => u.Email == email);

            if (!result.IsSuccess)
            {
                return Result<UserEntity>
                    .Failed(result.Error);
            }

            var verified = password.VerifyHash(result.Value.Password);

            return verified
                ? result
                : Result<UserEntity>
                    .Failed(new Error(HttpStatusCode.Unauthorized));
        }

        public async Task<Result<UserEntity>> ChangePassword(ChangePasswordModel model)
        {
            if (model.ConfirmedPassword != model.NewPassword)
            {
                return Result<UserEntity>
                    .Failed(new Error(HttpStatusCode.PreconditionFailed));
            }

            var verified = await Login(model.Email, model.Password);

            if (!verified.IsSuccess)
            {
                return verified;
            }

            verified.Value.Password = model.NewPassword.Hash();

            return await _UserRepository.UpdateAsync(verified.Value);
        }



        public void CheckForThrow(Error error)
        {
            _Logger.LogCritical(error.Code.ToString());

            if (error.Code != HttpStatusCode.OK)
                throw error.Code switch
                {
                    HttpStatusCode.BadRequest => new BadHttpRequestException(error.Code.ToString()),
                    HttpStatusCode.NotModified => new BadHttpRequestException(error.Code.ToString()),
                    HttpStatusCode.UnprocessableEntity => new GuidException(error.Code.ToString()),
                    HttpStatusCode.NotImplemented => new NotImplementedException(error.Code.ToString()),
                    HttpStatusCode.Ambiguous => new DuplicateException(error.Code.ToString()),
                    HttpStatusCode.NotFound => new NotFoundException(error.Code.ToString()),
                    HttpStatusCode.Unauthorized => new UnauthorizedException(error.Code.ToString()),
                    HttpStatusCode.PreconditionFailed => new UnauthorizedException(error.Code.ToString()),
                    _ => new Exception()
                };
        }

        private async Task<Guid> CreateUserAsync(UserModel model)
        {
            var entity = model.ToEntity();

            var request = await _UserRepository.GetByAsync(u => u.Id == entity.Id);

            if (request.Error.Code != HttpStatusCode.NotFound)
            {
                CheckForThrow(request.Error);
            }

            var result = await _UserRepository.AddAsync(entity);

            CheckForThrow(result.Error);

            return result.Value.Id;
        }

        private async Task<Guid> CreateInvoiceAsync(InvoiceModel model)
        {
            var entity = model.ToEntity();

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            var user = await GetUserAsync(entity.Id.ToString());
            if(user.IsSuccess)
                    CheckForThrow(new Error(HttpStatusCode.AlreadyReported));
            
            var result = await _InvoiceRepository.AddAsync(entity);

            CheckForThrow(result.Error);

            return result.Value.Id;
        }

        private async Task<Result<HttpStatusCode>> DeleteUserAsync(string publicKey)
        {
            var result = await GetUserAsync(publicKey);

            if (result.IsSuccess)
            {
                return await _UserRepository.DeleteAsync(result.Value);
            }

            _Logger.LogInformation(result.Error.Code.ToString());

            return Result<HttpStatusCode>.Deleted();
        }

        private async Task<Result<HttpStatusCode>> DeleteInvoiceAsync(string publicKey)
        {
            var result = await GetInvoiceAsync(publicKey);

            if (result.IsSuccess)
            {
                return await _InvoiceRepository.DeleteAsync(result.Value);
            }

            _Logger.LogInformation(result.Error.Code.ToString());

            return Result<HttpStatusCode>.Deleted();
        }

        private static Guid ValidateGuid(string key)
        {
            var valid = Guid.TryParse(key, out var guid);

            return valid ? guid : Guid.Empty;
        }

    }
}