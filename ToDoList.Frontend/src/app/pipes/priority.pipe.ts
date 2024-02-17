import {Pipe, PipeTransform} from '@angular/core';
import {Priority} from "../../enum/Priority.enum";

@Pipe({
  name: 'priority'
})
export class PriorityPipe implements PipeTransform {

  transform(value: Priority): string {
    switch (value) {
      case Priority.Easy: return "Easy";
      case Priority.Medium: return "Medium";
      case Priority.Hard: return "Hard";
      default: return "Error...";
    }
  }

}
