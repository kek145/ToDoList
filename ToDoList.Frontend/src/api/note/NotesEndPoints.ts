import { IQueryParameters } from "src/models/parameters/IQueryParameters.model";

export const NotesEndPoints = {
    createNote: "api/notes/create",
    getById: (id: number) => `api/notes/${id}`,
    getAllNotes: (parameters: IQueryParameters) => `api/notes?PageNumber=${parameters.pageNumber}&PageSize=${parameters.pageSize}`,
}