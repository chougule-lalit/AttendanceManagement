import {Component, Inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CommonService} from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-create-and-update-user',
  templateUrl: './create-and-update-user.component.html'
})
export class CreateAndUpdateUserComponent implements OnInit {

  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;
  roles: any[] = [];
  designationHolder: any[] = [];
  departmentHolder: any[] = [];

  constructor(
    public dialogRef: MatDialogRef<CreateAndUpdateUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const phoneRegex = /^[0-9]{10}$/;
    this.form = this.fb.group({
      id: [null],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern(emailRegex)]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]],
      roleId: ['', [Validators.required]],
      designationId: ['', [Validators.required]],
      departmentId: ['', [Validators.required]],
      password: ['', [(this.data) ? Validators.nullValidator : Validators.required]]
    });

    this.getRoles();
    this.getDesignation();
    this.getDepartment();
    if (this.data) {
      // console.log('Edit Data : ', this.data);
      this.mode = 'Update';

      this.commonService.getRequestWithId('UserMaster/getUser', this.data.id).subscribe((result) => {
        this.form.patchValue({
          id: result.id,
          firstName: result.firstName,
          lastName: result.lastName,
          email: result.email,
          phone: result.phone,
          designationId: result.designationId,
          departmentId: result.departmentId,
          roleId: result.roleId
        });
      });
      // console.log('patchValue : ', this.form.value);
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  getRoles(): void {
    this.commonService.getRequest('RoleMaster/getRoleDropdown').subscribe((result) => {
      this.roles = result;
      // console.log('Roles : ', this.roles);
    });
  }
  getDesignation(): void {
    this.commonService.getRequest('DesignationAndDepartment/getDesignationDropdown').subscribe((result) => {
      this.designationHolder = result;
      // console.log('designationHolder : ', this.designationHolder);
    });
  }
  getDepartment(): void {
    this.commonService.getRequest('DesignationAndDepartment/getDepartmentDropdown').subscribe((result) => {
      this.departmentHolder = result;
      // console.log('departmentHolder : ', this.departmentHolder);
    });
  }

  onSubmit(): void {
    // console.log('Form Data : ', this.form.value);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }

    this.commonService.createOrUpdateUser(this.form.value).subscribe((resp) => {
      // console.log('User Save Resp', resp);
      this.dialogRef.close(true);
    });
  }
}
