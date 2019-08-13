export class Task {
    TaskId: number;
    ParentTask: Task;
    ParentTaskName : string;
    ParentTaskId : number;
    TaskName: string;
    StartDate: Date;
    EndDate: Date;
    Priority: number;
}
