import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-verify-session',
    templateUrl: './verify-component.html',
    styleUrls: ['./verify-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class VerifyComponent {

    email: string;
    otp: string;
    password: string;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private apiService: ApiService
    ) {
        if (this.route.snapshot.paramMap.get('email')) {
            this.email = this.route.snapshot.paramMap.get('email');
        }
    }
    // 99e969
    verify(form: NgForm) {
        // alert(form.valid);
        if (form.valid) {
            this.apiService.put('Users/ResetPassword', form.value).subscribe(res => {
                console.log(res);
                // console.log(res);
                if (res.returnStatus) {
                    // form.reset();
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage, "OK");
                    this.router.navigate(['/authentication/login']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, "OK")
                }
            }, err => {
                console.log(err);  
                // form.reset();
                // form.resetForm();
                this.apiService.openSnackBar(err, "OK");
                // this.router.navigate(['/authentication/login']);

            })
        }
        // this.router.navigate(['/']);
    }

}



