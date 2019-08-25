import { Pipe, PipeTransform } from '@angular/core';
import { Task } from '../Models/Task';

@Pipe({
  name: 'searchTask'
})
export class SearchTaskPipe implements PipeTransform {

  transform(tasks: Task[], projectName: string): any {

    if (tasks && tasks.length) {
      var t = tasks.filter
        (task => {
          if (projectName) {
            if (task.Project) {
              if (task.Project.ProjectName.toLowerCase().indexOf(projectName.toLowerCase()) === -1) {
                return false;

              }
            }
            else
            {return false;}
          }
          return true;
        }
        );
    }
    return t;
  }

}
