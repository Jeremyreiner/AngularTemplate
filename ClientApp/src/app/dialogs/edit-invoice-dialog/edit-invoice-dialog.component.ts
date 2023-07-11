import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Invoice} from '../../models/Invoice';

@Component({
  selector: 'app-edit-invoice-dialog',
  templateUrl: './edit-invoice-dialog.component.html',
  styleUrls: ['./edit-invoice-dialog.component.scss']
})
export class EditInvoiceDialogComponent {

  invoice: Invoice;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Invoice,
              public dialogRef: MatDialogRef<EditInvoiceDialogComponent>) {
    this.invoice = data;
  }

  ngOnInit(): void {
  }

  protected readonly close = close;

  closeDialog() {
    this.dialogRef.close();
  }
}
