import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
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

  constructor(private taskService: TaskService) {}

  protected submitForm(taskRequest: ITaskModel): void {
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
