import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonService } from 'src/app/shared/services/common.service';
import { CreateAndUpdateModalComponent } from '../../inquiry/create-and-update-modal/create-and-update-modal.component';

@Component({
  selector: 'app-role-form',
  templateUrl: './role-form.component.html'
})
export class RoleFormComponent implements OnInit {


  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;
  constructor(
    public dialogRef: MatDialogRef<CreateAndUpdateModalComponent>,
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
      this.commonService.getRequestByIdWithParams('RoleMaster/getRole', this.data.id).subscribe((result) => {
        this.form.patchValue({
          id: this.data.id,
          name: result.name,
        })
      });
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
    this.commonService.postRequest('RoleMaster/createOrUpdate', this.form.value).subscribe((resp) => {
      // console.log('Save Resp', resp);
      this.dialogRef.close(true);
    })

  }


}
