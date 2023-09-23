import { Priority } from "src/enums/priority.enum";

export interface ITaskModel {
    title: string;
    description: string;
    priority: Priority;
    deadline: Date
}