import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from '@angular/forms';
import { ApiService } from '../../api.service'
import { UUID } from 'angular2-uuid';
@Component({
    selector: 'ms-register-session',
    templateUrl: './register-component.html',
    styleUrls: ['./register-component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class RegisterComponent {

    firstName: string = '';
    email: string = '';
    password: string = '';  
    phoneNumber: string;  

    company: string;
    companySizeId: string = ''; 

    companySizes: any = [];

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private apiService: ApiService
    ) {
        // alert(this.invitedBy)
        // this.route.paramMap.

        this.apiService.get('CompanySize/GetAllCompanySizes').subscribe(companySizes => {
            this.companySizes = companySizes;
        })
    }

    register(form: NgForm) {
        // alert(form.valid);

        if (form.valid && this.companySizeId) {

            let email = form.value.email     
            form.value.companySizeId = this.companySizeId;  
            this.apiService.register(form.value).subscribe(res => {
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
        } else {
            this.apiService.openSnackBar("Please fill all fields.", "OK")
        }
        // this.router.navigate(['/']);
    }

}



