import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {CommonService} from '../services/common.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private router: Router, private commonService: CommonService) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): any {
    if (route.data.expectedRoles == JSON.parse(localStorage.getItem('user-details')!).role) {
      return true;
    }
    alert('Access Denied!');
    return false;
  }
}
