import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CommonService} from "../../../../shared/services/common.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-attendance-type-form',
  templateUrl: './attendance-type-form.component.html'
})
export class AttendanceTypeFormComponent implements OnInit {

  form!: FormGroup;
  mode = 'Add';
  isSubmitted = false;
  usersHolder: any[] = [];
  attendanceTypeHolder: any[] = [];
  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    this.getUsersByRoleId(JSON.parse(localStorage.getItem('user-details')).role)
    this.form = this.fb.group({
      id: [null],
      userId: ['1', [Validators.required]],
      attendanceDate: ['', [Validators.required]],
      leaveFromDate: ['', [Validators.required]],
      leaveToDate: ['', [Validators.required]],
      timeIn: ['', [Validators.required]],
      timeOut: ['', [Validators.required]],
      attendanceTypeId: ['', [Validators.required]],
      description: [''],
      attendDate: ['', [Validators.required]],
    });

    if (this.data) {
      this.mode = 'Update';
      this.commonService.getRequestWithId('', this.data.id).subscribe((result) => {
        this.form.patchValue({
          id: result.id,
          userId: result.userId,
          attendanceDate: new Date(result.attendanceDate).toISOString().split('T')[0],
          leaveFromDate:  new Date(result.leaveFromDate).toISOString().split('T')[0],
          leaveToDate:  new Date(result.leaveToDate).toISOString().split('T')[0],
          timeIn: result.timeIn,
          timeOut: result.timeOut,
          attendanceTypeId: result.attendanceTypeId,
          description: result.description,
          attendDate: result.attendDate,
        });
      });
    }
  }

  get f() {
    return this.form.controls;
  }

  getUsersByRoleId(roleId: number) {
    this.commonService.getRequestWithId('', roleId).subscribe((result) => {
      if(result){
        this.usersHolder = result;
      }
    })
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('', this.form.value).subscribe((resp) => {
      this.dialogRef.close(true);
    })

  }

}
