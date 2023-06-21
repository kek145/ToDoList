import { Priority } from "../enums/priority.enum";

export interface ITaskModel {
    title: string;
    description: string;
    status: boolean;
    priority: Priority;
    created: Date;
}