import { Priority } from "../enums/priority.enum";

export interface ITaskModel {
    id: number;
    title: string;
    description: string;
    status: boolean;
    priority: Priority.Easy;
    deadline: string;
  }
  