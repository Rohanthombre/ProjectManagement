import { Observable } from 'rxjs';
import { Task } from '../Models/task';
import { User } from '../Models/user';



export abstract class IProjectManagementService {
    abstract getAllTask(): Observable<Task[]>;

    abstract getTaskById(taskId: number): Observable<Task>;

    abstract createTask(createTaskModel: Task): Observable<Task>;

    abstract editTask(editTaskComponent: Task): Observable<Task>;

    abstract getAllUsers(): Observable<User[]>;

    abstract getUserById(userId: number): Observable<User>;

    abstract createUser(user: User): Observable<User>;

    abstract editUser(editUser: User): Observable<User>;

}