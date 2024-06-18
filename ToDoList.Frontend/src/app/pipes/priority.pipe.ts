import { Pipe, PipeTransform } from '@angular/core';
import { Priority } from '../../enum/Priority.enum';

@Pipe({
  name: 'priority'
})
export class PriorityPipe implements PipeTransform {

  transform(value: Priority): string {
    switch (value) {
      case Priority.Easy: return 'Легкий';
      case Priority.Medium: return 'Середній';
      case Priority.Hard: return 'Жорсткий';
      default: return 'Error...';
    }
  }
}
