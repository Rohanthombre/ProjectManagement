export interface EditTaskModel {
    TaskId: number;
    TaskName: string;
    Priority?: number;
    StartDate?: Date;
    EndDate?: Date;
    ParentTaskId?: number;
}