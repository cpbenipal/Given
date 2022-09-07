import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-login-session',
    templateUrl: './login-component.html',
    styleUrls: ['./login-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class LoginComponent {

    email: string;
    password: string;

    constructor(
        private router: Router,
        private apiService: ApiService
    ) { }

    login(form: NgForm) {
        // alert(form.valid);
        if (form.valid) {
            let email = form.value.email
            this.apiService.login(form.value).subscribe(res => {
                console.log(res);
                if (res.returnStatus) {
                    // form.reset();
                    form.resetForm();
                    localStorage.setItem('isLogin', '1');
                    localStorage.setItem('role', res.returnData.role);
                    localStorage.setItem('userData', JSON.stringify(res.returnData));
                    this.apiService.openSnackBar(res.returnMessage, "OK");
                    this.router.navigate(['/home']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, "OK")
                }
            }, err => {
                    console.log(err);   
                let errorMessage = '';
                if (err.error instanceof ErrorEvent) {
                    // client-side error   
                    errorMessage = `c-s Error Code: ${err.error.message}`;

                } else {
                    // server-side error  
                    errorMessage = `s-s Error Code: ${err.status}\nMessage: ${err.message}`;
                }
                this.apiService.openSnackBar(errorMessage, "OK");  
            })
        }
        // this.router.navigate(['/']);
    }


}



