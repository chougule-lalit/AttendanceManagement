import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {CommonService} from "../../../shared/services/common.service";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {MatDialog} from "@angular/material/dialog";
import {AttendanceTypeFormComponent} from "./attendance-type-form/attendance-type-form.component";

@Component({
  selector: 'app-attendance-types',
  templateUrl: './attendance-types.component.html'
})
export class AttendanceTypesComponent implements OnInit {

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
    this.commonService.postRequest('', input).subscribe((result) => {
      if(result){
        this.dataSource = result.items;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }



  add(): void {
    const dialogRef = this.dialog.open(AttendanceTypeFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(AttendanceTypeFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('', id).subscribe((data) => {
      this.getData();
    });
  }

}
