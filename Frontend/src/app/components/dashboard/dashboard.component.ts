import { ITaskModel } from 'src/app/models/task.model';
import { MatDialog } from '@angular/material/dialog';
import { TaskService } from 'src/app/services/task.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { TaskAddEditComponent } from '../task-add-edit/task-add-edit.component';
import { Priority } from 'src/app/enums/priority.enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  private _taskModel: any = {
    title: "",
    description: "",
    priority: Priority.Easy,
    deadline: "",
  }

  protected items: ITaskModel[] = [];
  protected isLoading: boolean = true;

  dataSource: MatTableDataSource<ITaskModel> = new MatTableDataSource<ITaskModel>();
  displayedColumns: string[] = ['id', 'title', 'description', 'status', 'priority', 'deadline', 'action'];
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  pagination: {
    currentPage: number;
    pages: number;
  } = {
    currentPage: 1,
    pages: 1,
  };

  constructor(
    private taskService: TaskService, 
    private _dialog: MatDialog
  ) {}

  public ngOnInit(): void {
    this.isLoading = false;
    this.loadPage();
  }

  protected openAddedFormDialog(): void {
    this._dialog.open(TaskAddEditComponent);
  }

  protected openEditFormDialog(data: any): void {
    this._dialog.open(TaskAddEditComponent, {
      data
    });
  }

  private loadPage(): void {
    this.taskService.getAllTasks(this.pagination.currentPage).subscribe(
      (data: { items: ITaskModel[], currentPage: number, pages: number }) => {
        this.dataSource.data = data.items;
        this.pagination.currentPage = data.currentPage;
        this.pagination.pages = data.pages;
        this.paginator.length = data.pages * 13;
      },
      (error: any) => {
        console.log("Error:", error);
      }
    );
  }

  protected completedTask(id: number): void {
    this.taskService.completedTask(id).subscribe(
      (res: any) => {
        this.loadPage();
      },
      (error) => {
        console.log("Error");
      }
    );
  }

  protected getFailedTask(): void {
    this.taskService.getFailedTasks(this.pagination.currentPage).subscribe(
      (data: { items: ITaskModel[], currentPage: number, pages: number }) => {
        this.dataSource.data = data.items;
        this.pagination.currentPage = data.currentPage;
        this.pagination.pages = data.pages;
        this.paginator.length = data.pages * 13;
      },
      (error: any) => {
        console.log("Error:", error);
      }
    );
  }

  protected getCompletedTask(): void {
    this.isLoading = true;
    this.taskService.getCompletedTasks(this.pagination.currentPage).subscribe(
      (data: { items: ITaskModel[], currentPage: number, pages: number }) => {
        this.dataSource.data = data.items;
        this.pagination.currentPage = data.currentPage;
        this.pagination.pages = data.pages;
        this.paginator.length = data.pages * 13;
      },
      (error: any) => {
        console.log("Error:", error);
      }
    );
  }

  protected onPageChange(event: PageEvent): void {
    this.pagination.currentPage = event.pageIndex + 1;
    this.loadPage();
  }

  protected getTaskStatus(status: boolean): string {
    switch(status) {
      case false:
        return 'Not Done';
      case true:
        return 'Done';
    }
  }

  protected deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe(
      (res: any) => {
        this.loadPage();
      },
      (error) => {
        console.log("Error");
      }
    );
  }
}
