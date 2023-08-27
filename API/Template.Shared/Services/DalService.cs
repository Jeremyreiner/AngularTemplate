using System.Net;
using Template.Shared.Entities;
using Template.Shared.Enums;
using Template.Shared.Extensions;
using Template.Shared.Interfaces.IRepositories;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;

namespace Template.Shared.Services
{
    public class DalService : IDalService
    {
        private readonly IInvoiceRepository _InvoiceRepository;

        public DalService(IInvoiceRepository invoiceRepository)
        {
            _InvoiceRepository = invoiceRepository;
        }

        public async Task<Guid> CreateManagerAsync(InvoiceModel model)
        {
            if (model.AutoCreate)
                model = AutoFillInvoice();

            var entity = model.ToEntity();

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            await _InvoiceRepository.AddAsync(entity);

            return entity.Id;

        }

        public async Task<Guid> DeleteManagerAsync(InvoiceModel model) =>
            await _InvoiceRepository.DeleteAsync(model.ToEntity());

        public async Task<Guid> UpdateManagerAsync(InvoiceModel model) =>
            await _InvoiceRepository.UpdateAsync(model.ToEntity());

        public async Task<InvoiceEntity?> GetInvoiceAsync(string id) =>
            await _InvoiceRepository.GetByAsync(id, u => u.Id == Guid.Parse(id));

        public async Task<List<InvoiceEntity>> GetAllInvoices() => await _InvoiceRepository.GetListByAsync();

        public async Task InvoiceTimeEventManagerAsync(CancellationToken ct)
        {
            var status = await RemovePaidInvoices();

            await UpdateUnpaidInvoices();
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

        private async Task<HttpStatusCode> RemovePaidInvoices()
        {
            var result = await GetAllInvoices();

            if (!result.Any())
                return HttpStatusCode.BadRequest;

            var invoices = result
                .Where(i => i.Status == StatusEnum.Paid)
                .ToList();

            await _InvoiceRepository.DeleteRangeAsync(invoices);

            return HttpStatusCode.OK;
        }

        private async Task UpdateUnpaidInvoices()
        {
            var result = await GetAllInvoices();

            if (!result.Any())
                return;

            var enumMap = new Dictionary<int, StatusEnum>
            {
                { 0, StatusEnum.Overdue },
                { 1, StatusEnum.Paid }
            };

            var invoices = result.Where(i => i.Status != StatusEnum.Paid).ToList();

            foreach (var invoice in invoices)
                invoice.Status = enumMap[Faker.RandomNumber.Next(0, 1)];

            await _InvoiceRepository.UpdateRangeAsync(invoices);
        }

    }
}