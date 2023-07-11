import { Component } from '@angular/core';
import {Constants} from '../../models/Constants';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

    protected readonly Constants = Constants;
}
