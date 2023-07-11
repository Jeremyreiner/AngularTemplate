import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusColor'
})
export class HttpStatusPipe implements PipeTransform {
  transform(status?: number): string {
    switch (status) {
      case 200:
      case 201:
        return 'green';
      default:
        return 'red';
    }
  }
}
