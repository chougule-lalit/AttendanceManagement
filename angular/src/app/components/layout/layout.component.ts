import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/shared/interfaces/customer';
import { AuthService } from 'src/app/shared/services/auth.service';
import { CustomerService } from 'src/app/shared/services/customer.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  customers: Customer[] = [];

  constructor(private customerService: CustomerService, private authService: AuthService) {
  }

  ngOnInit(): void {
    // this.getCustomers();
  }

  getCustomers(): void {
    this.customerService.getTasks().subscribe(result => {
      this.customers = result;
    });
  }

  get isLoggedIn(): any {
    return JSON.parse(localStorage.getItem('user-details')!);
  }

  logOut(): void {
    this.authService.logOut();
  }

}
