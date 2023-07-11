import {InjectionToken, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ApiService} from './services/api.service';
import {HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {HttpStatusPipe} from './pipes/HttpStatusPipe';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select';
import {PostInvoiceComponent} from './components/post-invoice/post-invoice.component';
import {GetInvoicesComponent} from './components/get-invoices/get-invoices.component';
import {MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import {EditInvoiceDialogComponent} from './dialogs/edit-invoice-dialog/edit-invoice-dialog.component';
import {InvoiceStatusColorPipe, InvoiceStatusPipe} from './pipes/InvoiceStatusPipe';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {ThemeSwitchComponent} from './components/theme-switch/theme-switch.component';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatToolbarModule} from '@angular/material/toolbar';
import {HeaderComponent} from './components/header/header.component';
import {ToastService} from './services/toast.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';

@NgModule({
  declarations: [
    AppComponent,
    HttpStatusPipe,
    PostInvoiceComponent,
    GetInvoicesComponent,
    EditInvoiceDialogComponent,
    InvoiceStatusPipe,
    InvoiceStatusColorPipe,
    ThemeSwitchComponent,
    HeaderComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatDialogModule,
    MatIconModule,
    MatMenuModule,
    MatButtonToggleModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule
  ],
  providers: [
    MatDialog,
    ToastService,
    MatSnackBar,
    {
      provide: MAT_DIALOG_DATA,
      useValue: {}
    },
    {
      provide: MatDialogRef,
      useValue: {}
    },
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
