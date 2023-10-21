import { Component, Inject } from '@angular/core';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-add-edit',
  templateUrl: './task-add-edit.component.html',
  styleUrls: ['./task-add-edit.component.scss']
})
export class TaskAddEditComponent {
  taskModel: any = {
    title: "",
    description: "",
    priority: Priority.Easy,
    deadline: "",
  }

  constructor(private taskService: TaskService, @Inject(MAT_DIALOG_DATA) public data: any, private _dialog: MatDialog) {}

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
}
