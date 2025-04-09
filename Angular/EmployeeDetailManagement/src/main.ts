import { EmployeeDetailComponent } from './app/employee-detail/employee-detail.component';
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideRouter } from '@angular/router';

bootstrapApplication(AppComponent, {
 providers:[
  provideHttpClient(withInterceptorsFromDi()), 
  provideRouter([
    { path: '', redirectTo: 'employeedetail', pathMatch: 'full' },
    { path: 'employeedetail', loadComponent: () => import('./app/employee-detail/employee-detail.component').then(m => m.EmployeeDetailComponent) }
  ])
  
 ]
}).catch(err => console.error(err));
