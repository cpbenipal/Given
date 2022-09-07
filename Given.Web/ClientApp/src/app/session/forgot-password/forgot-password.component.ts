import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-forgot-password',
    templateUrl: './forgot-password-component.html',
    styleUrls: ['./forgot-password-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class ForgotPasswordComponent {

    email: string;

    constructor(
        private router: Router,
        private apiService: ApiService
    ) { }

    sendOtp(form: NgForm) {
        // alert(form.valid);
        if (form.valid) {
            let email = form.value.email
            this.apiService.put('Users/ForgotPassword', form.value).subscribe(res => {
                console.log(res);
                if (res.returnStatus) {
                    // form.reset();
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage, "OK");
                    this.router.navigate(['/authentication/confirm-and-update-password', { email: email }]);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, "OK")
                }
            }, err => {
                console.log(err);

                // form.resetForm();
                this.apiService.openSnackBar(err, "OK");
                // this.router.navigate(['/authentication/confirm-and-update-password', { email: email }]);

            })
        }
        // this.router.navigate(['/']);
    }
}



