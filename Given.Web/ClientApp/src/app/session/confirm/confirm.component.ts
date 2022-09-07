import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-confirm-session',
    templateUrl: './confirm-component.html',
    styleUrls: ['./confirm-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class ConfirmComponent {

    email: string;
    otp: string;
    password: string;
    companyId: string = this.route.snapshot.params.companyid;

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
            let email = form.value.email
            form.value.companyId = this.companyId;  

            this.apiService.confirmAccount(form.value).subscribe(res => {
                console.log(res);
                if (res.returnCode == 200) {
                    // form.reset();
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage, "OK");
                    this.router.navigate(['/authentication/login']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, "OK")
                }
            }, err => {
                    this.apiService.openSnackBar(err, "OK")
            })
        }
        // this.router.navigate(['/']);
    }

}



