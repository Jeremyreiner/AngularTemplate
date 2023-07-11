import {StatusEnum} from './StatusEnum';
export class Invoice {
  id: string;
  invoiceNumber: string;
  date: Date;
  status: StatusEnum;
  totalAmount: number;
  vat: number;
}
