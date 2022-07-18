import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonService } from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-attendance-form',
  templateUrl: './attendance-form.component.html'
})
export class AttendanceFormComponent implements OnInit {
  form!: FormGroup;
  mode = 'Add';
  isSubmitted = false;
  usersHolder: any[] = [];
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
    });

    if (this.data) {
      console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.commonService.getRequestWithId('Attendance/get', this.data.id).subscribe((result) => {
        console.log('Get Data by id : ', new Date(result.attendanceDate));
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
        });
      });


      // console.log('patchValue : ', this.form.value);

    }
  }

  get f() {
    return this.form.controls;
  }

  getUsersByRoleId(roleId: number) {
    this.commonService.getRequestWithId('UserMaster/getUserListPerRoleDropDown', roleId).subscribe((result) => {
      console.log('Users By role : ', result);
      if(result){
        this.usersHolder = result;
      }
    })
  }

  onSubmit(): void {
    console.log('Form Data : ', this.form.valid);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('Attendance/createOrUpdate', this.form.value).subscribe((resp) => {
      console.log('Save Resp', resp);
      this.dialogRef.close(true);
    })

  }

}
