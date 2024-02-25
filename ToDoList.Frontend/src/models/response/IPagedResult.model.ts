export interface IPagedResultModel<T> {
    totalCount: number;
    pageNumber: number;
    recordPage: number;
    totalPages: number;
    items: T[];
}