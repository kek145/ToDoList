import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { INoteRequestModel } from 'src/models/request/INoteRequest.model';
import { IBaseResponseModel } from 'src/models/response/IBaseResponse.model';
import { INoteResponseModel } from 'src/models/response/INoteResponse.model';
import { NotesEndPoints } from '../note/NotesEndPoints';
import { IQueryParametersModel } from 'src/models/parameters/IQueryParameters.model';
import { Priority } from 'src/enum/Priority.enum';
import {IPagedResultModel} from "../../models/response/IPagedResult.model";

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(private httpClient: HttpClient) { }

  // POST requests
  public createNote(note: INoteRequestModel) : Observable<IBaseResponseModel<INoteResponseModel>> {
    return this.httpClient.post<IBaseResponseModel<INoteResponseModel>>(`${environment.httpUrlApi}${NotesEndPoints.createNote}`, note, { withCredentials: true });
  }

  // GET requests
  public getNoteById(noteId: number): Observable<IBaseResponseModel<INoteResponseModel>> {
    return this.httpClient.get<IBaseResponseModel<INoteResponseModel>>(`${environment.httpUrlApi}${NotesEndPoints.getById(noteId)}`);
  }

  public getAllNotes(queryParameters: IQueryParametersModel): Observable<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>> {
    return this.httpClient.get<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>>(`${environment.httpUrlApi}${NotesEndPoints.getAllNotes(queryParameters)}`, { withCredentials: true });
  }

  public getFailedNotes(queryParameters: IQueryParametersModel): Observable<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>> {
    return this.httpClient.get<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>>(`${environment.httpUrlApi}${NotesEndPoints.getFailedNotes(queryParameters)}`, { withCredentials: true });
  }

  public getCompletedNotes(queryParameters: IQueryParametersModel): Observable<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>> {
    return this.httpClient.get<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>>(`${environment.httpUrlApi}${NotesEndPoints.getCompletedNotes(queryParameters)}`, { withCredentials: true });
  }

  public getNotesByPriority(queryParameters: IQueryParametersModel, priority: Priority): Observable<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>> {
    return this.httpClient.get<IBaseResponseModel<IPagedResultModel<INoteResponseModel>>>(`${environment.httpUrlApi}${NotesEndPoints.getByPriorityNotes(queryParameters, priority)}`, { withCredentials: true });
  }

  // PATCH requests
  public completeNote(noteId: number) : Observable<void> {
    return this.httpClient.patch<void>(`${environment.httpUrlApi}${NotesEndPoints.completeNote(noteId)}`, { withCredentials: true });
  }

  // PUT requests
  public updateNote(noteId: number, note: INoteRequestModel) : Observable<void> {
    return this.httpClient.put<void>(`${environment.httpUrlApi}${NotesEndPoints.updateNote(noteId)}`, note, { withCredentials: true });
  }

  // DELETE requests
  public deleteNote(noteId: number) : Observable<void> {
    return this.httpClient.delete<void>(`${environment.httpUrlApi}${NotesEndPoints.deleteNote(noteId)}`, { withCredentials: true });
  }
}
