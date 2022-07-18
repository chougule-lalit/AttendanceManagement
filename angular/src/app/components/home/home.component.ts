import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CommonService} from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  form!: FormGroup;
  mode = 'Create';
  isSubmitted = false;

  constructor(
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
      remark: ['']
    });
    // console.log('patchValue : ', this.form.value);
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
    this.commonService.postRequest('Enquiry/createOrUpdate', this.form.value).subscribe((resp) => {
      // console.log('Save Resp', resp);
      this.isSubmitted = false;
      this.form.reset();
    })

  }
}
