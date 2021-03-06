import { Pipe, PipeTransform } from '@angular/core';
import { User } from '../Models/user';

@Pipe({
  name: 'commonSearch'
})
export class CommonSearchPipe implements PipeTransform {

  transform(inputValues: any[], fields: string[],searchText:string): any {
    if (fields===null || fields.length===0 || !searchText || searchText.trim() == "") {
      return inputValues;
    }
    var a=inputValues.filter(value=>{
      var matches=false;
      fields.forEach(field=>{
        if(!matches)
        {
          var fieldValue=value[field].toLowerCase() as string;
          matches=fieldValue.startsWith(searchText.toLocaleLowerCase()); 
        }
      });
      return matches;
    });
    return a;
  }

}
