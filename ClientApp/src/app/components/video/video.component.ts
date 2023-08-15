import { Component } from '@angular/core';
import {Constants} from '../../models/Constants';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.scss']
})
export class VideoComponent {
    protected readonly Constants = Constants;
}
