import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'statusTranslate'
})

export class StatusTranslatePipe implements PipeTransform {
    
    private readonly translations: { [key: string]: string } = {
        'Success': 'Успіх',
        'Error': 'Помилка'
      };
    
      transform(value: string): string {
        return this.translations[value] || value;
      }
}