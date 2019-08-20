import { Pipe, PipeTransform } from '@angular/core';
import { Task } from './Models/Task';



@Pipe({
  name: 'taskQuery'
})
export class TaskQueryPipe implements PipeTransform {

  transform(tasks: Task[], projectName: string)
  : Task[] {
    
    
    if (tasks && tasks.length) {
      var t = tasks.filter
        (task => {
          if (projectName && task.Project.ProjectName.toLowerCase().indexOf(projectName.toLowerCase()) === -1) {
            return false;
          }
          
          return true;
        }
        );
    }
    return t;

  }

}
