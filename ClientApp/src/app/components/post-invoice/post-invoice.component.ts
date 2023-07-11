import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Invoice} from '../../models/Invoice';
import {ApiService} from '../../services/api.service';
import {StatusEnum} from '../../models/StatusEnum';
import {Constants} from '../../models/Constants';

@Component({
  selector: 'app-post-invoice',
  templateUrl: './post-invoice.component.html',
  styleUrls: ['./post-invoice.component.scss']
})
export class PostInvoiceComponent {

  isLoading =  false;
  _invoice = new Invoice();
  statusOptions: string[] = [];
  update = false;
  message = '';

  protected readonly Constants = Constants;

  constructor(
    private api: ApiService,
  ) {
    this.statusOptions = Constants.getEnumValues(StatusEnum);
  }

  @Input() set invoice(invoice: Invoice) {
    if (invoice) {
      this._invoice = invoice;
      this.update = true;
    }
  }

  @Output() submit = new EventEmitter<boolean>();


  async postInvoice() {
    this.message = '';
    this.isLoading = true;
    let response;
    if (this.update) {
      response = await this.api.updateInvoice(this._invoice);
      this.submit.emit(true);
    } else {
      if(this.api.invoices.find(i => i.invoiceNumber === this._invoice.invoiceNumber)) {
        this.message = 'invoice number already exists';
        this.isLoading = false;
        return;
      }
      this._invoice.id = Constants.guidNull;
      if(this._invoice.date !== null && this._invoice.status !== null) {
        response = await this.api.createInvoice(this._invoice);
      }
    }
    console.log(response);
    this.isLoading = false;
  }


}
