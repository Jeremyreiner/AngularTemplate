import {Pipe, PipeTransform} from '@angular/core';
import {StatusEnum} from '../models/StatusEnum';

@Pipe({
  name: 'InvoiceStatus'
})
export class InvoiceStatusPipe implements PipeTransform {
  transform(status?: StatusEnum): string {
    switch (status) {
      case StatusEnum.paid:
        return 'PAID';
      case StatusEnum.unpaid:
        return 'UNPAID';
        case StatusEnum.overdue:
        return 'OVERDUE';
      default:
        return '';
    }
  }
}


@Pipe({
  name: 'InvoiceStatusColor'
})
export class InvoiceStatusColorPipe implements PipeTransform {
  transform(status?: StatusEnum): string {
    switch (status) {
      case StatusEnum.paid:
        return 'green';
      case StatusEnum.unpaid:
        return 'orange';
      case StatusEnum.overdue:
        return 'red';
      default:
        return '';
    }
  }
}
