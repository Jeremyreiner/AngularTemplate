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
        private readonly IInvoiceRepository _InvoiceRepository;

        readonly ILogger<DalService> _Logger;

        public DalService(
            ILogger<DalService> logger, IInvoiceRepository invoiceRepository)
        {
            _Logger = logger;
            _InvoiceRepository = invoiceRepository;
        }

        public async Task<Guid> CreateManagerAsync(object model)
        {
            var invoice = (InvoiceModel)model;

            if (invoice.AutoCreate)
                invoice = AutoFillInvoice();

            return await CreateInvoiceAsync(invoice);
        }

        public async Task<HttpStatusCode> DeleteManagerAsync(object model)
        {
            var invoice = (InvoiceModel)model;

            var result = await DeleteInvoiceAsync(invoice.Id.ToString());

            return result.Status;
        }

        public async Task<Guid> UpdateManagerAsync(object model)
        {
            var invoice = (InvoiceModel)model;

            var response = await _InvoiceRepository.UpdateAsync(invoice.ToEntity());

            return response.Value.Id;
        }

        public async Task<Result<InvoiceEntity>> GetInvoiceAsync(string id)
        {
            var guid = id.ValidGuid();

            if (guid != Guid.Empty)
                return await _InvoiceRepository.GetByAsync(id, u => u.Id == guid);

            return Result<InvoiceEntity>.Failed(new Error(HttpStatusCode.UnprocessableEntity));
        }

        public async Task<Result<List<InvoiceEntity>>> GetAllInvoices() => await _InvoiceRepository.GetListByAsync();


        public void CheckForThrow(Error error)
        {
            _Logger.LogInformation("Error Status: {0}", error.Code);

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

        private InvoiceModel AutoFillInvoice() =>
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = Guid.NewGuid().ToString(),
                Status = StatusEnum.Unpaid,
                TotalAmount = Faker.RandomNumber.Next(0, 100),
                Vat = Faker.RandomNumber.Next(0, 20),
                Date = DateTime.Now
            };

        private async Task<Guid> CreateInvoiceAsync(InvoiceModel model)
        {
            var entity = model.ToEntity();

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            var result = await _InvoiceRepository.AddAsync(entity);

            CheckForThrow(result.Error);

            return result.Value.Id;
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
    }
}