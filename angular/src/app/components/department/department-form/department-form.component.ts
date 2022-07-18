import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CommonService} from "../../../shared/services/common.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html'
})
export class DepartmentFormComponent implements OnInit {

  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private commonService: CommonService
  ) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [null],
      name: ['', [Validators.required]],
    });

    if (this.data) {
      // console.log('Edit Data : ', this.data);
      this.mode = 'Update';
      this.commonService.getRequestWithId('DesignationAndDepartment/getDepartment', this.data.id).subscribe((result) => {
        if (result) {
          this.form.patchValue({
            id: this.data.id,
            name: result.name,
          });
        }
      });
      // console.log('patchValue : ', this.form.value);
    }
  }

  get f() {
    return this.form.controls;
  }

  onSubmit(): void {
    // console.log('Form Data : ', this.form.value);
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('DesignationAndDepartment/createOrUpdateDerpartment', this.form.value).subscribe((resp) => {
      // console.log('Save Resp', resp);
      this.dialogRef.close(true);
    })

  }

}
