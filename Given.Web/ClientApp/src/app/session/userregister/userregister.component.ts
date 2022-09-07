import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
import { UUID } from 'angular2-uuid';
@Component({
    selector: 'ms-userregister-session',
    templateUrl: './userregister-component.html',
    styleUrls: ['./userregister-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class UserRegisterComponent {

    firstName: string;
    email: string;
    password: string;
    lastName: string;
    phoneNumber: string;
    otp: string;

    userId: string = this.route.snapshot.params.id;
    invitedBy: string = this.route.snapshot.params.invitedBy;
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private apiService: ApiService
    ) {

    }

    register(form: NgForm) {
        // alert(form.valid);
        if (form.valid) {
            let email = form.value.email
            form.value.id = this.userId;
            form.value.invitedBy = this.invitedBy;
            this.apiService.userregister(form.value).subscribe(res => {
                console.log(res);
                if (res.returnStatus) {
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



