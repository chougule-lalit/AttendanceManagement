import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AttendanceComponent } from './attendance/attendance.component';
import { AttendanceFormComponent } from './attendance/attendance-form/attendance-form.component';
import { DesignationComponent } from './designation/designation.component';
import { DepartmentComponent } from './department/department.component';
import { DesignationFormComponent } from './designation/designation-form/designation-form.component';
import { DepartmentFormComponent } from './department/department-form/department-form.component';
import { AttendanceReportsComponent } from './attendance/attendance-reports/attendance-reports.component';
import { AttendanceTypesComponent } from './attendance/attendance-types/attendance-types.component';
import { AttendanceTypeFormComponent } from './attendance/attendance-types/attendance-type-form/attendance-type-form.component';


@NgModule({
  declarations: [
    LayoutComponent,
    AttendanceComponent,
    AttendanceFormComponent,
    DesignationComponent,
    DepartmentComponent,
    DesignationFormComponent,
    DepartmentFormComponent,
    AttendanceReportsComponent,
    AttendanceTypesComponent,
    AttendanceTypeFormComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    HttpClientModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatPaginatorModule,
  ]
})
export class MainModule { }
