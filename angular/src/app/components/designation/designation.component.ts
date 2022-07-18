import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {CommonService} from "../../shared/services/common.service";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {MatDialog} from "@angular/material/dialog";
import {DesignationFormComponent} from "./designation-form/designation-form.component";

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html'
})
export class DesignationComponent implements OnInit {

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
    this.commonService.postRequest('DesignationAndDepartment/fetchDesignationList', input).subscribe((result) => {
      // console.log('Get Data : ', result);
      if(result){
        this.dataSource = result.items;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }

  add(): void {
    const dialogRef = this.dialog.open(DesignationFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      // console.log('The dialog was closed after insert : ', result);
      if(result){
        this.getData();
      }
    });
  }

  edit(editData: any): void {
    // console.log('Edit Data : ', editData);
    const dialogRef = this.dialog.open(DesignationFormComponent, {
      data: editData,
    });
    dialogRef.afterClosed().subscribe(result => {
      // console.log('The dialog was closed after update : ', result);
      if(result){
        this.getData();
      }
    });
  }

  delete(id: any): void {
    this.commonService.deleteRequestWithId('DesignationAndDepartment/deleteDesignation', id).subscribe((data) => {
      // console.log('Delete Resp : ', data);
      this.getData();
    });
  }
}
