import { Component } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { debug } from 'console';

@Component({
  selector: 'app-employee-detail',
  standalone: true, 
  imports: [],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css'
})
export class EmployeeDetailComponent {

  employees: any[] = [];

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    alert("Success")
    this.loadEmployees();
  }


  loadEmployees() {
    this.employeeService.getAllEmployees().subscribe((data:any) => {
      debugger
      this.employees = data;
    });
  }
}
