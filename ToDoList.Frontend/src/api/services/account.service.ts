import {Observable} from "rxjs";
import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {AccountEndPoints} from "../account/AccountEndPoints";
import { IUserFullNameResponseModel } from "../../models/response/IUserFullNameResponseModel";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  public getUserFullName(): Observable<IUserFullNameResponseModel> {
    return this.httpClient.get<IUserFullNameResponseModel>(`${environment.httpUrlApi}${AccountEndPoints.fullName}`,{ withCredentials : true });
  }
}
