import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from '../../api.service'
import { NgForm } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'ms-companyedit',
    templateUrl:'./companyedit-component.html',
    styleUrls: ['./companyedit-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class CompanyEditComponent implements OnInit {
    userId: any = this.apiService.getUserDetail('id')
    companyId: any;
    phone: string;
    fax: string;
    streetLine1: string;
    streetLine2: string;
    city: string;
    state: string;
    zip: string;
    website: string;    
    description: string; 
    companySizeId: any; 
    companyName: string; 
    companySizes: any = [];
    constructor(public apiService: ApiService, private router: Router,) { }

    ngOnInit() {
        this.apiService.get('CompanySize/GetAllCompanySizes').subscribe(companySizes => {
            this.companySizes = companySizes;
        })                                           
        this.companyId = this.apiService.getUserDetail('companyId');
        this.apiService.get('Company/GetCompanyById/'+this.companyId).subscribe(res => {
            // this.userDetail = res;
            this.phone = res.phone;
            this.fax = res.fax;
            this.streetLine1 = res.streetLine1;
            this.streetLine2 = res.streetLine2;
            this.city = res.city;
            this.state = res.state;    
            this.zip = res.zip;    
            this.website = res.website;    
            this.description = res.description;    
            this.companySizeId = res.companySize.id;    
            this.companyName = res.companyName;
        }, err => {

        })

        
    } 

    updateProfile(data:NgForm){
        data.value.userId = this.userId;
        data.value.companyId = this.companyId;
        data.value.companySizeId = this.companySizeId;
        let index = this.companySizes.findIndex(ele=>ele.id == this.companySizeId);
        data.value.companySize = this.companySizes[index];
        console.log(data.value);
        this.apiService.put('Company/PutCompany',data.value).subscribe(res=>{
            if(res.returnStatus){
                this.apiService.updateProfileByKey('company',this.companyName);
                this.apiService.openSnackBar(res.returnMessage,'OK');
                this.router.navigateByUrl('/home')
            }else{
                this.apiService.openSnackBar(res.returnMessage,'OK')
            }
        },err=>{
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



