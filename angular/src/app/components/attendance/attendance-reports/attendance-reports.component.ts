import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {CommonService} from "../../../shared/services/common.service";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {AttendanceFormComponent} from "../attendance-form/attendance-form.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-attendance-reports',
  templateUrl: './attendance-reports.component.html'
})
export class AttendanceReportsComponent implements OnInit {

  displayedColumns = ['id', 'attendanceDate', 'leaveFromDate', 'leaveToDate', 'timeIn', 'timeOut', 'attendDate'];
  dataSource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  pastMonthHolder: any[] = [];

  constructor(private commonService: CommonService, private fb: FormBuilder, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getPastMonth();
  }

  getData(pastMonth: { month: number, year: number, lable: string }): void {
    const payload = {
      pastMonth: {
        month: pastMonth.month,
        year: pastMonth.year,
        lable: pastMonth.lable
      },
      userId: JSON.parse(localStorage.getItem('user-details')).id
    };
    this.commonService.postRequest('Attendance/fetchAttendanceReport', payload).subscribe((result) => {
      console.log('Get Data : ', result);
      if (result && result.length > 0) {
        this.dataSource = result;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }else{
        this.dataSource = [];
      }
    });
  }

  getPastMonth() {
    this.commonService.getRequest('Attendance/getPastMonthDropdown').subscribe((result) => {
      console.log('Past Month : ', result);
      this.pastMonthHolder = result;
    })
  }

  onSelectedPastMonth(event: any) {
    let val = event.target.value;
    this.pastMonthHolder.filter((item) => {
      if (item.lable == val) {
        console.log('Filter Item : ', item);
        let pastMonth = {
          month: item.month,
          year: item.year,
          lable: item.lable
        };
        this.getData(pastMonth);
      }
    })
  }

  // add(): void {
  //   const dialogRef = this.dialog.open(AttendanceFormComponent);
  //   dialogRef.afterClosed().subscribe(result => {
  //     // console.log('The dialog was closed after insert : ', result);
  //     if (result) {
  //       this.getData();
  //     }
  //   });
  // }
  //
  // edit(editData: any): void {
  //   // console.log('Edit Data : ', editData);
  //   const dialogRef = this.dialog.open(AttendanceFormComponent, {
  //     data: editData,
  //   });
  //   dialogRef.afterClosed().subscribe(result => {
  //     // console.log('The dialog was closed after update : ', result);
  //     if (result) {
  //       this.getData();
  //     }
  //   });
  // }
  //
  // delete(id: any): void {
  //   this.commonService.deleteRequestWithId('Attendance/delete', id).subscribe((data) => {
  //     // console.log('Delete Resp : ', data);
  //     this.getData();
  //   });
  // }
}
