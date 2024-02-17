import { Priority } from "src/enum/Priority.enum";

export interface INoteRequestModel {
    title: string;
    description: string;
    priority: string;
    deadline: string;
}
