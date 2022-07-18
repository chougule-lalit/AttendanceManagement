import {Component, OnInit} from '@angular/core';
import {CommonService} from '../shared/services/common.service';
import {AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from 'src/app/shared/helpers/must-match-validators';
import {AuthService} from 'src/app/shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoginFormSubmitted = false;
  registerForm!: FormGroup;
  isRegisterFormSubmitted = false;
  loginOrRegister = true;
  loginError = {status: false, message: ''};
  roles: any[] = [];
  designationHolder: any[] = [];
  departmentHolder: any[] = [];
  constructor(
    private commonService: CommonService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    if (localStorage.getItem('user-details')) {
      this.router.navigate(['/main/home']);
    }
  }

  ngOnInit(): void {
    this.initLoginForm();
    this.initRegisterForm();
    this.getRoles();
    this.getDesignation();
    this.getDepartment();
  }

  initLoginForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  get loginFormControls(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
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


  onLoginFormSubmit(): void {
    this.loginError = {
      status: false,
      message: ''
    };
    console.log('Login Form Data : ', this.loginForm.value);
    this.isLoginFormSubmitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe((data) => {
      console.log('Login Resp : ', data);
      if (data.isSuccess) {
        this.router.navigate(['/main/home']);
      } else {
        this.loginError = {
          status: true,
          message: 'Email / Password Incorrect'
        };
      }
    });
  }

  initRegisterForm(): void {
    const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const phoneRegex = /^[0-9]{10}$/;
    const formOptions: AbstractControlOptions = {
      validators: MustMatch('password', 'conf_password')
    };
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern(emailRegex)]],
      phone: ['', [Validators.required, Validators.pattern(phoneRegex)]],
      roleId: ['', [Validators.required]],
      designationId: ['', [Validators.required]],
      departmentId: ['', [Validators.required]],
      password: ['', [Validators.required]],
      conf_password: ['', [Validators.required]],
    }, formOptions);
  }

  get registerFormControls(): { [key: string]: AbstractControl } {
    return this.registerForm.controls;
  }

  onRegisterFormSubmit(): void {
    console.log('Register Form Data : ', this.registerForm.value);
    this.isRegisterFormSubmitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    let formData = {
      ...this.registerForm.value
    };
    delete formData.conf_password;
    this.commonService.createOrUpdateUser(formData).subscribe((resp) => {
      console.log('Register Resp : ', resp);
      this.loginOrRegister = true;
    });
  }


  toggleForm(): void {
    this.loginOrRegister = !this.loginOrRegister;
  }

}
