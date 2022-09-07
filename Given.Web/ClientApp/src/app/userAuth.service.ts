import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthApiService {

    constructor(private router: Router, private http: HttpClient) {



    }

    getUserDetail(key) {
        let userDetail = JSON.parse(localStorage.getItem('userData'));
        return userDetail[key];
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const isLoggedIn = localStorage.getItem('isLogin'); // ... your login logic here
        if (isLoggedIn) {
            if (this.getUserDetail('role') == 'User') {
               this.router.navigate(['/error/401']);
               return false;   
            }else{
                return true;
            }
        } else {
            this.router.navigate(['/authentication/login']);
            return false;
        }
    }


}