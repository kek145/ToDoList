import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';

@Component({
  selector: 'app-task-add-edit',
  templateUrl: './task-add-edit.component.html',
  styleUrls: ['./task-add-edit.component.scss']
})
export class TaskAddEditComponent {
  task: ITaskModel = {
    id: 0,
    title: "",
    description: "",
    status: false,
    priority: Priority.Easy,
    deadline: "",
  }

}
