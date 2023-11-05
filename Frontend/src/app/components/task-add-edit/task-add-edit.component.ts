import { Component, Inject, OnInit } from '@angular/core';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-add-edit',
  templateUrl: './task-add-edit.component.html',
  styleUrls: ['./task-add-edit.component.scss']
})
export class TaskAddEditComponent implements OnInit {
  taskModel: any = {
    id: 0,
    title: "",
    description: "",
    priority: Priority.Easy,
    deadline: "",
  }

  constructor(private taskService: TaskService, @Inject(MAT_DIALOG_DATA) public data: any, private _dialog: MatDialog,  private _dialogRef: MatDialogRef<TaskAddEditComponent>) {}
  ngOnInit(): void {
    this.taskService.getTaskById(this.data.id).subscribe((task: ITaskModel) => {
      this.data = task;
    });
  }

  protected submitForm(taskRequest: ITaskModel): void {
    if(this.data) {
      this.taskService.updateTask(this.data.id, this.taskModel).subscribe(
        (res: any) => {
          alert("ok");
        },
        (error: any) => {
          alert("error");
        }
      );
    }
    else {
      this.taskService.createTask(taskRequest).subscribe(
        (res: ITaskModel) => {
          alert("ggwp");
        },
        (error: any) => {
          alert("error");
        }
      );
    }
  }

  protected closeForm(): void {
    this._dialogRef.close();
  }
}
