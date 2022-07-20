import {AfterContentChecked, ChangeDetectorRef, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-attendance-form',
  templateUrl: './attendance-form.component.html'
})
export class AttendanceFormComponent implements OnInit, AfterContentChecked {
  form!: FormGroup;
  mode = 'Add';
  isSubmitted = false;
  usersHolder: any[] = [];
  currentDate!: string;
  isAttendanceTypePresent = false;

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService,
    private cd: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    const today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    this.currentDate = yyyy + '-' + String(mm).padStart(2, '0') + '-' + String(dd).padStart(2, '0');
    console.log('Current Date : ', this.currentDate);

    this.getUserList();
    this.form = this.fb.group({
      id: [null],
      userId: ['1', [Validators.required]],
      attendanceDate: [this.currentDate, [Validators.required]],
      leaveFromDate: [''],
      leaveToDate: [''],
      timeIn: [''],
      timeOut: [''],
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
          leaveFromDate: new Date(result.leaveFromDate).toISOString().split('T')[0],
          leaveToDate: new Date(result.leaveToDate).toISOString().split('T')[0],
          timeIn: result.timeIn,
          timeOut: result.timeOut,
          attendanceTypeId: result.attendanceTypeId,
          description: result.description,
        });
      });


      // console.log('patchValue : ', this.form.value);

    }

    // this.form.get('attendanceTypeId').valueChanges.subscribe(value => {
    //
    //   if (+value === 1) {
    //     console.log('VAlue : ', +value);
    //     this.form.get('timeIn').setValidators(Validators.required);
    //     this.form.get('timeOut').setValidators(Validators.required);
    //     this.form.get('leaveFromDate').clearValidators();
    //     this.form.get('leaveToDate').clearValidators();
    //   } else {
    //     console.log('VAlue : ', +value);
    //     this.form.get('timeIn').clearValidators();
    //     this.form.get('timeOut').clearValidators();
    //     this.form.get('leaveFromDate').setValidators(Validators.required);
    //     this.form.get('leaveToDate').setValidators(Validators.required);
    //   }
    //   this.form.updateValueAndValidity();
    //   this.cd.detectChanges();
    // });
  }

  ngAfterContentChecked(): void {
    this.cd.detectChanges();
  }

  get f() {
    return this.form.controls;
  }

  getUserList(): void {
    this.commonService.getRequest('UserMaster/getAllUserListDropDown').subscribe((result) => {
      this.usersHolder = result;
    });
  }

  onSubmit(): void {
    console.log('Form Data : ', this.form.controls);
    console.log('Form Data : ', this.form.valid);
    this.isSubmitted = true;
    if (+this.form.value.attendanceTypeId === 1 && !this.form.value.timeIn) {
      alert('Time In Field is Required');
      return;
    } else if (+this.form.value.attendanceTypeId !== 1 && !this.form.value.leaveFromDate) {
      alert('Leave From Date Field is Required');
      return;
    }
    if (+this.form.value.attendanceTypeId === 1 && !this.form.value.timeOut) {
      alert('Time Out Field is Required');
      return;
    } else if (+this.form.value.attendanceTypeId !== 1 && !this.form.value.leaveToDate) {
      alert('Leave to Date Field is Required');
      return;
    }
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('Attendance/createOrUpdate', this.form.value).subscribe((resp) => {
      console.log('Save Resp', resp);
      this.dialogRef.close(true);
    })
  }

  onAttendanceTypeChanged(event: any) {
    let type = +event.target.value;
    this.isAttendanceTypePresent = type === 1;
    this.form.updateValueAndValidity();
  }

}
