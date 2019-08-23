import { Injectable } from '@angular/core';
import { IProjectManagementService } from './IProjectManagementService';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Models/Task';
import { environment } from 'src/environments/environment';
import { User } from '../Models/user';
import { Project } from '../Models/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectmanagementService extends IProjectManagementService {


  constructor(private http: HttpClient) {
    super();
  }

  getAllTask(): Observable<Task[]> {
    return this.http.get<Task[]>(environment.ApiService + 'Task/GetTasks');
  }

  getTaskById(taskId: number): Observable<Task> {
    return this.http.get<Task>(environment.ApiService + 'Task/GetTask?id=' + taskId);
  }
  createTask(createTaskModel: Task): Observable<Task> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Task>(environment.ApiService + 'Task/CreateTask/', createTaskModel, httpOptions);
  }

  editTask(editTaskComponent: Task): Observable<Task> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<Task>(environment.ApiService + 'Task/EditTask/', editTaskComponent, httpOptions);
  }




  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(environment.ApiService + 'User/GetUsers');
  }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(environment.ApiService + 'User/GetUser?id=' + userId);

  }

  createUser(user: User): Observable<User> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<User>(environment.ApiService + 'User/CreateUser/', user, httpOptions);
  }

  editUser(editUser: User): Observable<User> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<User>(environment.ApiService + 'User/EditUser/', editUser, httpOptions);

  }
  deleteUser(id: number) {
    return this.http.delete(environment.ApiService + 'User/DeleteUser/' + id);
  }


  getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(environment.ApiService + 'Project/GetProjects');
  }

  getProjectById(projectId: number): Observable<Project> {
    return this.http.get<Project>(environment.ApiService + 'Project/GetProject?id=' + projectId);

  }

  createProject(project: Project): Observable<Project> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Project>(environment.ApiService + 'Project/CreateProject/', project, httpOptions);
  }

  editProject(editProject: Project): Observable<Project> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<Project>(environment.ApiService + 'Project/EditProject/', editProject, httpOptions);

  }
  deleteProject(id: number) {
    return this.http.delete(environment.ApiService + 'Project/DeleteProject/' + id);
  }
}
