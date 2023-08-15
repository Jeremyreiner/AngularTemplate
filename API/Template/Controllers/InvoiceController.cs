using Microsoft.AspNetCore.Mvc;
using Template.Shared.Entities;
using Template.Shared.Extensions;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;

namespace Template.Controllers;

[Route("[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IDalService _DalService;

    public InvoiceController(IDalService dalService)
    {
        _DalService = dalService;
    }

    [HttpGet("{id}")]
    public async Task<InvoiceEntity?> GetByAsync(string id) => await _DalService.GetInvoiceAsync(id);

    [HttpGet("All")]
    public async Task<List<InvoiceEntity>> GetAllAsync() => (await _DalService.GetAllInvoices());

    [HttpPost]
    public async Task<Guid> CreateAsync([FromBody] InvoiceModel model) => await _DalService.CreateManagerAsync(model);

    [HttpPut("Update")]
    public async Task<Guid> UpdateAsync([FromBody] InvoiceModel invoice) => await _DalService.UpdateManagerAsync(invoice);

    [HttpDelete]
    public async Task DeleteAsync([FromBody] InvoiceModel invoice) => await _DalService.DeleteManagerAsync(invoice);
}