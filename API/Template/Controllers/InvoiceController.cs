using Microsoft.AspNetCore.Mvc;
using Template.Shared.Entities;
using Template.Shared.Enums;
using Template.Shared.Interfaces.IServices;
using Template.Shared.Models;
using Template.Shared.Results;

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

    [HttpPost]
    public async Task<Guid> CreateAsync([FromBody] InvoiceModel model) => await _DalService.CreateManagerAsync(ClassType.Invoice, model);

    [HttpGet("{id}")]
    public async Task<Result<InvoiceEntity>> GetByAsync(string id) => await _DalService.GetInvoiceAsync(id);

    [HttpGet("All")]
    public async Task<Result<List<InvoiceEntity>>> GetAllAsync() => await _DalService.GetAllInvoices();

    [HttpPut("Update")]
    public async Task<Guid> UpdateAsync([FromBody] InvoiceModel invoice) => await _DalService.UpdateManagerAsync(ClassType.Invoice, invoice);

    [HttpDelete]
    public async Task DeleteAsync([FromBody] InvoiceModel invoice) => await _DalService.DeleteManagerAsync(ClassType.Invoice, invoice);
}