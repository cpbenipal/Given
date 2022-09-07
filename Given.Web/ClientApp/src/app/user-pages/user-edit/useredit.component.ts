import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from '../../api.service'
import { NgForm } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'ms-useredit',
    templateUrl: './useredit-component.html',
    styleUrls: ['./useredit-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class UserEditComponent implements OnInit {
    userId: any;
    userDetail: any;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    roleId: string;
    roleId_c: any = null;
    email: string;
    createdOn: string;
    allRoles: any = [];
    constructor(public apiService: ApiService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.userId = this.route.snapshot.params.id;
        this.apiService.getProfile(this.userId).subscribe(res => {
            this.userDetail = res;
            this.firstName = res.firstName
            this.lastName = res.lastName
            this.phoneNumber = res.phoneNumber
            this.roleId = res.roleId
            this.roleId_c = res.roleId
            this.email = res.email
            this.createdOn = moment(res.createdOn).format('DD-MM-YYYY HH:mm')
        }, err => {

        })

        this.apiService.get('Roles/GetAllRoles').subscribe(res => {
            res.forEach(role => {
                if (role.roleName != 'SuperAdmin') {
                    this.allRoles.push(role);
                }
            })
        }, err => {

        })
    }

    updateProfile(data: NgForm) {
        data.value.id = this.userId;
        if ((this.userDetail.role != 'SuperAdmin') && this.roleId != this.roleId_c) {

            data.value.userRole = [{ roleId: this.roleId }];
        } else {
            data.value.userRole = null;
        }

        delete data.value.email;
        delete data.value.createdOn;
        // data.removeControl(this.email);
        // data.removeControl(this.createdOn);
        this.apiService.put('Users/PutUser', data.value).subscribe(res => {
            if (res.returnStatus) {
                this.apiService.updateProfileByKey('firstName', this.firstName)
                this.apiService.updateProfileByKey('lastName', this.lastName)
                this.apiService.updateProfileByKey('phoneNumber', this.phoneNumber)
                this.apiService.openSnackBar(res.returnMessage, 'OK')
            } else {
                this.apiService.openSnackBar(res.returnMessage, 'OK')
            }
        }, err => {
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
}
