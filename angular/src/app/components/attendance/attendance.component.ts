import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {CommonService} from 'src/app/shared/services/common.service';
import {AttendanceFormComponent} from './attendance-form/attendance-form.component';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html'
})
export class AttendanceComponent implements OnInit {
  displayedColumns = ['id', 'attendanceDate', 'leaveFromDate', 'leaveToDate', 'timeIn', 'timeOut', 'attendDate', 'actions'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private commonService: CommonService, private fb: FormBuilder, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getData();

  }

  getData(): void {
    const input = {
      maxResultCount: 100,
      skipCount: 0,
    };
    this.commonService.postRequest('Attendance/fetchAttendanceDetailList', input).subscribe((result) => {
      if (result) {
        this.dataSource = result.items;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }


  add(): void {
    const dialogRef = this.dialog.open(AttendanceFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(AttendanceFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  delete(id: any): void {
    if (confirm('Are you sure! Delete this')) {
      this.commonService.deleteRequestWithId('Attendance/delete', id).subscribe((data) => {
        this.getData();
      });
    }
  }
}
