import { Injectable } from '@angular/core';
import { ITaskService } from './ITaskService';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Task } from './Models/Task';
import { environment } from 'src/environments/environment';
import { CreateTaskModel } from './Models/CreateTaskModel';
import { EditTaskModel } from './Models/EditTaskModel';
import { TaskServiceMockData } from './test-manager.service.mock.data';



@Injectable()
export class TaskserviceServiceFake extends ITaskService {


  constructor() {
    super();
  }


  getAllTask(): Observable<Task[]> {
    return of(TaskServiceMockData.Tasks);
  }

  getTaskById(taskId: number): Observable<Task> {
    return of(TaskServiceMockData.Task1);
  }
  
  createTask(createTaskModel: CreateTaskModel): Observable<CreateTaskModel> {
    
    return of(TaskServiceMockData.Task1);
  }

  editTask(editTaskComponent: EditTaskModel): Observable<EditTaskModel> {
   
   
    return of(TaskServiceMockData.Task1);

  }


}
