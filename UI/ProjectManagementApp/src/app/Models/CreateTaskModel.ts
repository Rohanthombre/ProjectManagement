export interface CreateTaskModel {
    TaskId?: number;
    TaskName: string;
    Priority?: number;
    StartDate?: Date;
    EndDate?: Date;
    ParentTaskId?: number;
}