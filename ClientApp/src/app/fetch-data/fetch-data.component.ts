import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoService } from 'src/todo.service';
import { Todo } from 'src/models/todo';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public dataFromServer: Todo[];

  _service: TodoService;

  constructor(service: TodoService) {

    this._service = service;

    this._service.getTasks().subscribe(result => {
      this.dataFromServer = result;
    }, error => console.error(error));
  }

  public changeState(task: Todo, state: string) {
    task.state = state;

    this._service.changeTaskState(task).subscribe(result => {
      this._service.getTasks().subscribe(tasks => {
        this.dataFromServer = tasks;
      }, error => console.error(error));
    }, error => console.log(error));
  }

  public delete(task: Todo) {

    this._service.deleteTask(task).subscribe(result => {
      this._service.getTasks().subscribe(tasks => {
        this.dataFromServer = tasks;
      }, error => console.error(error));
    }, error => console.log(error));
  }
}
