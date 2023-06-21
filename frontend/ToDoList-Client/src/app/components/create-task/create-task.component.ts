import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {
  task: ITaskModel;
  constructor(private titleService: Title) { 
    this.task = {
      title: '',
      description: '',
      status: false,
      priority: Priority.easy,
      created: new Date()
    };
  }

  ngOnInit(): void {
    this.titleService.setTitle('Create Task');
  }

  submitForm() {
    console.log(`title: ${this.task.title}, description: ${this.task.description}, status: ${this.task.status}, priority: ${this.task.priority}, created: ${this.task.created}`)
  }
}
