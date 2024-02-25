import { IBaseResponseModel } from "./IBaseResponse.model";

export interface IErrorResponseModel {
    error: IBaseResponseModel<object>;
}