import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from '../../api.service'
import { NgForm } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'ms-contactedit',
    templateUrl: './contactedit-component.html',
    styleUrls: ['./contactedit-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ContactEditComponent implements OnInit {
    userId: any;
    contactDetail: any;

    prefix: string = '';
    firstName: string = '';
    middleName: string = '';
    lastName: string = '';
    company: string = '';
    primaryEmail: string = '';
    secondaryEmail: string = '';
    mobile: string = '';
    phone: string = '';
    gender: string = '';
    address1: string = '';
    address2: string = '';
    city: string = '';
    zipCode: string = '';
    state: string = '';
    country: string = '';
    birthday: string = '';
    isActive: boolean = false;
    isAdd: boolean = false;
    today: any = new Date();

    contactId: any;
    // allRoles: any = [];
    constructor(public apiService: ApiService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        this.contactId = this.route.snapshot.params.id;
        this.apiService.get('Contacts/GetById/'+this.contactId).subscribe(res => {
            console.log(res);
            this.contactDetail = res;
            this.prefix = res.prefix;
            this.firstName = res.firstName;
            this.middleName = res.middleName;
            this.lastName = res.lastName;
            this.company = res.company;
            this.primaryEmail = res.primaryEmail;
            this.secondaryEmail = res.secondaryEmail;
            this.mobile = res.mobile;
            this.phone = res.phone;
            this.gender = res.gender;
            this.address1 = res.address1;
            this.address2 = res.address2;
            this.city = res.city;
            this.zipCode = res.zipCode;
            this.state = res.state;
            this.country = res.country;
            this.birthday = res.birthday;
            this.isActive = res.isActive;
            this.isAdd = false;
            this.userId = res.userId;
            
        }, err => {

        })

        
    }

    updateContact(form: NgForm) {
        if(form.valid && this.gender){
            form.value.id = this.contactId;
            form.value.userId = this.userId;
            form.value.gender = this.gender;
            form.value.isActive = this.isActive;
            form.value.isAdd = false;
            form.value.prefix = this.prefix;
            form.value.photo = "";
            this.apiService.put('Contacts/PutContact', form.value).subscribe(res => {
                if (res.returnStatus) {
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage,"OK");
                    this.router.navigate(['/contact/contactlist']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, 'OK')
                }
            }, err => {
                this.apiService.openSnackBar(err, 'OK')
            })
        }else{
          this.apiService.openSnackBar("Please fill all fields.","OK")
        }
    }
}
