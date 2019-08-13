import { Observable } from 'rxjs';
import { Task } from './Models/Task';
import { CreateTaskModel } from './Models/CreateTaskModel';
import { EditTaskModel } from './Models/EditTaskModel';



export abstract class ITaskService {
    abstract getAllTask(): Observable<Task[]>;

    abstract getTaskById(taskId: number): Observable<Task>;

    abstract createTask(createTaskModel: CreateTaskModel): Observable<CreateTaskModel>;

    abstract editTask(editTaskComponent: EditTaskModel): Observable<EditTaskModel>;
}