import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MyModel } from '../Model/myModel.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LlmService {

  private apiUrl = 'http://localhost:5190/api/ModelLLM';

  private readonly http = inject(HttpClient)
  

  getList(): Observable<MyModel[]> {
    return this.http.get<MyModel[]>(`${this.apiUrl}/list`);
  }


  getById(id: number): Observable<MyModel> {
    return this.http.get<MyModel>(`${this.apiUrl}/${id}`);
  }

  getFiltered(filter: string): Observable<MyModel[]> {
    return this.http.get<MyModel[]>(`${this.apiUrl}/filter`, { params: { filter } });
  }
}
