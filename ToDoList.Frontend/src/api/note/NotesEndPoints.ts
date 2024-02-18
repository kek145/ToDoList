import { Priority } from "src/enum/Priority.enum";
import { IQueryParametersModel } from "src/models/parameters/IQueryParameters.model";

export const NotesEndPoints = {
    // post requests
    createNote: "api/notes/create",

    // get requests
    getById: (id: number) => `api/notes/${id}`,
    getAllNotes: (parameters: IQueryParametersModel) => `api/notes?PageNumber=${parameters.pageNumber}&PageSize=${parameters.pageSize}`,
    getFailedNotes: (parameters: IQueryParametersModel) => `api/notes/failed?PageNumber=${parameters.pageNumber}&PageSize=${parameters.pageSize}`,
    getCompletedNotes: (parameters: IQueryParametersModel) => `api/notes/completed?PageNumber=${parameters.pageNumber}&PageSize=${parameters.pageSize}`,
    getByPriorityNotes: (parameters: IQueryParametersModel, priority: Priority) => `api/notes/${priority}/priority?PageNumber=${parameters.pageNumber}&PageSize=${parameters.pageSize}`,

    // patch requests
    completeNote: (id: number) => `api/notes/${id}/complete`,

    // put requests
    updateNote: (id: number) => `api/notes/${id}/update`,

    // delete requests
    deleteNote: (id: number) => `api/notes/${id}/delete`
}
