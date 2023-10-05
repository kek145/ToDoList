import { Priority } from "../enums/priority.enum";

export interface ITaskModel {
    taskId: number;
    title: string;
    description: string;
    status: boolean;
    priority: Priority;
    createdDate: Date;
}