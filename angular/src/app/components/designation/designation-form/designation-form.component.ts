import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CommonService} from "../../../shared/services/common.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CreateAndUpdateModalComponent} from "../../inquiry/create-and-update-modal/create-and-update-modal.component";

@Component({
  selector: 'app-designation-form',
  templateUrl: './designation-form.component.html'
})
export class DesignationFormComponent implements OnInit {

  form!: FormGroup;
  mode = 'Add';
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
      this.mode = 'Update';
      this.commonService.getRequestWithId('DesignationAndDepartment/getDesignation', this.data.id).subscribe((result) => {
        if (result) {
          this.form.patchValue({
            id: this.data.id,
            name: result.name,
          });
        }
      });
    }
  }

  get f() {
    return this.form.controls;
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.form.invalid) {
      return;
    }
    this.commonService.postRequest('DesignationAndDepartment/createOrUpdateDesignation', this.form.value).subscribe((resp) => {
      this.dialogRef.close(true);
    })

  }
}
