import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {CommonService} from "../../shared/services/common.service";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {MatDialog} from "@angular/material/dialog";
import {DepartmentFormComponent} from "./department-form/department-form.component";

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html'
})
export class DepartmentComponent implements OnInit {

  displayedColumns = ['id', 'name', 'actions'];
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
    this.commonService.postRequest('DesignationAndDepartment/fetchDepartmentList', input).subscribe((result) => {
      if(result){
        this.dataSource = result.items;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(DepartmentFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.getData();
      }
    });
  }

  edit(editData: any): void {
    const dialogRef = this.dialog.open(DepartmentFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.getData();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('DesignationAndDepartment/deleteDepartment', id).subscribe((data) => {
      this.getData();
    });
  }

}
