import { HttpStatusCode } from "@angular/common/http";

export interface IBaseResponseModel<T> {
    statusCode: HttpStatusCode;
    message: string;
    data: T
    errors: string[]
}