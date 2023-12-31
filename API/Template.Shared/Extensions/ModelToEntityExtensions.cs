﻿using Template.Shared.Models;
using Template.Shared.Entities;

namespace Template.Shared.Extensions;


public static class ModelToEntityExtensions
{
    public static InvoiceEntity ToEntity(this InvoiceModel model) =>
        new()
        {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            Date = model.Date,
            Status = model.Status,
            Vat = model.Vat,
            TotalAmount = model.TotalAmount
        };
}