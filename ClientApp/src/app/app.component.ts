import {Component, Inject, OnInit} from '@angular/core';
import {Constants} from './models/Constants';
import {ApiService} from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  constructor(public api: ApiService) {
  }

  protected readonly Constants = Constants;


  async ngOnInit() {
    await this.api.getAllInvoices();
  }

}
