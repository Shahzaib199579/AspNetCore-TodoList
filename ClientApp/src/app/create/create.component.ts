import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoService } from 'src/todo.service';

@Component({
  selector: 'app-create-component',
  templateUrl: './create.component.html'
})
export class CreateTaskComponent {

  _service: TodoService;

  constructor(service: TodoService) {
    this._service = service;
  }

  public taskName = '';

  public createNewTask() {
    this._service.createNewTask(this.taskName).subscribe(result => {
      console.log(result);
    }, error => console.error(error));

    this.taskName = '';
  }
}
