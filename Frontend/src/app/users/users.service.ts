import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Lists } from './users.component';

@Injectable({
  providedIn: 'root'
})

export class UsersService {
    constructor(private http: HttpClient) {
    }
    importAndGetData(): Observable<any> {
        return this.http.get<any>(environment.baseUrl+"/values");
    }
}