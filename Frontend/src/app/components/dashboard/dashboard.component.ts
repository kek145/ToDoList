import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator'; // Добавьте PageEvent
import { ITaskModel } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  dataSource: MatTableDataSource<ITaskModel> = new MatTableDataSource<ITaskModel>();
  displayedColumns: string[] = ['id', 'title', 'description', 'status', 'priority', 'deadline'];
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  pagination: {
    currentPage: number;
    pages: number;
  } = {
    currentPage: 1,
    pages: 1,
  };

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadPage();
  }

  loadPage(): void {
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

  onPageChange(event: PageEvent): void {
    this.pagination.currentPage = event.pageIndex + 1;
    this.loadPage();
  }
}
