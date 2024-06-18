import {MatDialog} from "@angular/material/dialog";
import {HttpStatusCode} from "@angular/common/http";
import {MatTableDataSource} from "@angular/material/table";
import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {NotesService} from "../../../api/services/notes.service";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {IPagedResultModel} from "../../../models/response/IPagedResult.model";
import {IBaseResponseModel} from "../../../models/response/IBaseResponse.model";
import {INoteResponseModel} from "../../../models/response/INoteResponse.model";
import {IErrorResponseModel} from "../../../models/response/IErrorResponse.model";
import {IQueryParametersModel} from "../../../models/parameters/IQueryParameters.model";
import {Priority} from "../../../enum/Priority.enum";
import {enumHelper} from "../../../helpers/enum.helper";
import {CreateUpdateComponent} from "../create-update/create-update.component";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements AfterViewInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  totalCount: number = 0;

  public priority: Priority = Priority.Easy;
  public priorityEnum = Priority;
  public enumValues = enumHelper;

  protected priority1 = {
    'Easy': 'Легкий'
  }

  public showButtons: boolean = true;

  public dataSource!: MatTableDataSource<INoteResponseModel>;


  public displayedColumns: string[] = ['title', 'description', 'priority', 'status', 'deadline', 'action'];

  public parameters: IQueryParametersModel = {
    pageNumber: 1,
    pageSize: 10
  };

  private currentTab: string = 'all';


  constructor(private noteService: NotesService, private dialog: MatDialog) { }

  ngAfterViewInit(): void {
    this.getAllNotes(this.parameters)
  }

  onPageChange(event: PageEvent): void {
    this.parameters.pageNumber = event.pageIndex + 1;
    this.parameters.pageSize = event.pageSize;
    this.loadTabData();
  }

  public onPriorityChange(): void {
    this.parameters.pageNumber = 1;
    this.getAllNotesByPriority(this.parameters, this.priority);
  }

  public getAllNotes(parameters: IQueryParametersModel) : void {
    this.showButtons = true;
    this.noteService.getAllNotes(parameters).subscribe({
      next: (_response: IBaseResponseModel<IPagedResultModel<INoteResponseModel>>) => {
        this.totalCount = _response.data.totalCount;
        this.dataSource = new MatTableDataSource<INoteResponseModel>(_response.data.items);

        this.currentTab = 'all';
      },
      error: (_error: IErrorResponseModel) => {
        alert("error");
      }
    });
  }

  public getAllCompletedNotes(parameters: IQueryParametersModel): void {
    this.showButtons = false;
    this.noteService.getCompletedNotes(parameters).subscribe({
      next: (_response: IBaseResponseModel<IPagedResultModel<INoteResponseModel>>) => {
        this.totalCount = _response.data.totalCount;
        this.dataSource = new MatTableDataSource<INoteResponseModel>(_response.data.items);
        this.currentTab = 'completed'
      },
      error: (_error: IErrorResponseModel) => {
        alert("error");
      }
    });
  }

  public getAllFailedNotes(parameters: IQueryParametersModel): void {
    this.showButtons = false;
    this.noteService.getFailedNotes(parameters).subscribe({
      next: (_response: IBaseResponseModel<IPagedResultModel<INoteResponseModel>>) => {
        this.totalCount = _response.data.totalCount;
        this.dataSource = new MatTableDataSource<INoteResponseModel>(_response.data.items);

        this.currentTab = 'failed'
      },
      error: (_error: IErrorResponseModel) => {
        alert("error");
      }
    });
  }

  public getAllNotesByPriority(parameters: IQueryParametersModel, priorityNote: Priority): void {
    this.showButtons = true;
    this.noteService.getNotesByPriority(parameters, priorityNote).subscribe({
      next: (_response: IBaseResponseModel<IPagedResultModel<INoteResponseModel>>) => {
        this.totalCount = _response.data.totalCount;
        this.dataSource = new MatTableDataSource<INoteResponseModel>(_response.data.items);

        this.currentTab = 'priority'
      },
      error: (_error: IErrorResponseModel) => {
        alert("error");
      }
    });
  }

  public completeNote(noteId: number) : void {
    this.noteService.completeNote(noteId).subscribe({
      next: (_response: any): void => {
        this.dataSource.data = this.dataSource.data.filter(note => note.id !== noteId);
        this.totalCount--;

        if (this.dataSource.data.length === 0) {
          this.loadTabData();
        }
      },
      error: (_error: any): void => {
        alert(noteId);
      }
    });
  }
  public deleteNote(noteId: number): void {
    this.noteService.deleteNote(noteId).subscribe({
      next: (_response: any): void => {
        this.dataSource.data = this.dataSource.data.filter(note => note.id !== noteId);
        this.totalCount--;

        if (this.dataSource.data.length === 0) {
          this.loadTabData();
        }
      },
      error: (_error: HttpStatusCode): void => {
        alert(noteId);
      }
    });
  }

  public updateNote(noteId: number): void {
    const dialogRef = this.dialog.open(CreateUpdateComponent, {
      width: '1200px',
      height: '700px',
      data: { action: 'Update', noteId: noteId }
    });

    dialogRef.afterClosed().subscribe((_result: any) => {
      console.log('The error modal was closed');
    });
  }

  private loadTabData(): void {
    switch (this.currentTab) {
      case 'all':
        this.getAllNotes(this.parameters);
        break;
      case 'completed':
        this.getAllCompletedNotes(this.parameters);
        break;
      case 'failed':
        this.getAllFailedNotes(this.parameters);
        break;
      case 'priority':
        this.getAllNotesByPriority(this.parameters, this.priority);
        break;
      default:
        break;
    }
  }

}
