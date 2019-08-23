import { Project } from './project';

export class Task {
    TaskId: number;
    TaskName: string;
    ParentTask: ParentTask;
    Project: Project;
    StartDate: Date;
    EndDate: Date;
    Priority: number;
    Status : string;
    UserId : number;
}

export class ParentTask
{
     ParentTaskId : number;
     ParentTaskName : string;
}

