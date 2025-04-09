import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { debug } from 'console';
import { Observable } from 'rxjs';
export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  createdDate?: Date;
}
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private apiUrl = 'https://localhost:7140/api/Employee';

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<Employee[]> {

    debugger

    return this.http.get<Employee[]>(`${this.apiUrl}/Get-all-Employee-Detail`);
  }
}
