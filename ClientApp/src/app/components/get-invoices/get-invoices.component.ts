import {Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {Invoice} from '../../models/Invoice';
import {MatDialog} from '@angular/material/dialog';
import {EditInvoiceDialogComponent} from '../../dialogs/edit-invoice-dialog/edit-invoice-dialog.component';
import {Constants} from '../../models/Constants';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';

@Component({
  selector: 'app-get-invoices',
  templateUrl: './get-invoices.component.html',
  styleUrls: ['./get-invoices.component.scss']
})
export class GetInvoicesComponent implements OnInit, OnChanges {

  @ViewChild(MatPaginator) myPaginator: MatPaginator;
  @Input() invoices: Invoice[];
  @Output() refresh = new EventEmitter<boolean>();


  total = 0;
  totalVat = 0;

  dataSource = new MatTableDataSource<Invoice>();

  displayedColumns: string[] = ['invoiceNumber', 'id', 'status', 'date', 'vat', 'totalAmount', 'actions'];
  protected readonly Constants = Constants;

  constructor(private dialog: MatDialog, private api: ApiService) {
  }

  ngOnChanges() {
    this.calculateTotal();
  }

  async ngOnInit() {
    this.calculateTotal();
  }

  refreshInvoices() {
    this.invoices = null;
    this.refresh.emit(true);
  }

  initTable() {
      this.dataSource = new MatTableDataSource(this.invoices);
      this.dataSource.paginator = this.myPaginator;
      if (this.myPaginator) {
        this.myPaginator.length = this.invoices.length;
      }
  }

  calculateTotal() {
    if (this.invoices?.length) {
      this.total = this.invoices.map(i => i.totalAmount).reduce((a, b) => a + b, 0);
      this.totalVat = this.invoices.map(i => i.vat).reduce((a, b) => a + b, 0);
    }
    this.initTable();
  }

  async editInvoice(invoice: Invoice) {
    this.dialog.open(EditInvoiceDialogComponent, {data: invoice}).afterClosed();
    this.calculateTotal();
  }

  async delete(invoice: Invoice) {
    await this.api.deleteInvoice(invoice);
    this.calculateTotal();
  }
}
