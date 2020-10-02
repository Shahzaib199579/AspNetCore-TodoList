import { Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from './models/todo';
import { Observable } from 'rxjs';

const todo = 'Todo';

export class TodoService {

    request: HttpClient;
    url: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.request = http;
        this.url = baseUrl;
    }

    public createNewTask(taskName: string) {
        return this.request.post(this.url + todo + '/Create', {title: taskName});
    }

    public getTasks(): Observable<Todo[]> {
        return this.request.get<Todo[]>(this.url + todo);
    }

    public changeTaskState(task: Todo) {
        return this.request.post(this.url + todo + '/ChangeTaskState', task);
    }

    public deleteTask(task: Todo) {
        return this.request.post(this.url + todo + '/DeleteTask', task);
    }
}
