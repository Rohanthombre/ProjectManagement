import { Task } from './Models/Task';



export class TaskServiceMockData {


    public static Task1: Task = {
        TaskId: 1, TaskName: "Task 1", ParentTaskId: null, ParentTaskName : null , Priority: 1, 
        StartDate: new Date(2018, 5, 5), EndDate: new Date(2018, 5, 5), ParentTask: null
    };

    public static Task2: Task = {
        TaskId: 1, TaskName: "Task 2", ParentTaskId: 1, ParentTaskName : "Task 1" , Priority: 1, 
        StartDate: new Date(2018, 5, 5), EndDate: new Date(2018, 5, 5), ParentTask: null
    };

    public static Task3: Task = {
        TaskId: 1, TaskName: "Task 3", ParentTaskId: 2, ParentTaskName : "Task 2" , Priority: 1, 
        StartDate: new Date(2018, 5, 5), EndDate: new Date(2018, 5, 5), ParentTask: null
    };

    public static Task4: Task = {
        TaskId: 1, TaskName: "Task 4", ParentTaskId: 3, ParentTaskName : "Task 3" , Priority: 1, 
        StartDate: new Date(2018, 5, 5), EndDate: new Date(2018, 5, 5), ParentTask: null
    };

    
    public static Tasks: Task[] = [
        TaskServiceMockData.Task1, TaskServiceMockData.Task2, TaskServiceMockData.Task3,  TaskServiceMockData.Task4
    ];

}