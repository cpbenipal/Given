import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from '../../api.service'
import { NgForm } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'ms-designationedit',
    templateUrl: './designationedit-component.html',
    styleUrls: ['./designationedit-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class DesignationEditComponent implements OnInit {
    designationDetail: any = [];
    designationId: string = '';
    categoryId: string = '';
    name: string = '';
      
    status: boolean = false;
      
    userId: string = '';
    allCategories: any = [];
    
    constructor(public apiService: ApiService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        this.apiService.get('Category/GetAllCategory').subscribe(res=>{
          this.allCategories = res;
        })
        
        this.designationId = this.route.snapshot.params.id;
        this.apiService.get('Designation/GetById/'+this.designationId).subscribe(res => {
            console.log(res);
            this.designationDetail = res;
            this.categoryId = res.categoryId;
            this.name = res.name;
            this.status = res.status;
            this.userId = res.userId;
            
        }, err => {

        })

        
    }

    updateDesignation(form: NgForm) {
        if(form.valid  && this.categoryId){
            form.value.id = this.designationId;
            form.value.userId = this.userId;
            form.value.createdBy = this.userId;
            form.value.updatedBy = this.apiService.getUserDetail('id');
            form.value.status = this.status;
            form.value.isAdd = false;
            form.value.categoryId = this.categoryId;


            this.apiService.put('Designation/PutDesignation', form.value).subscribe(res => {
                if (res.returnStatus) {
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage,"OK");
                    this.router.navigate(['/designation/designationlist']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, 'OK')
                }
            }, err => {
                this.apiService.openSnackBar("Please fill all mendatory fields.","OK")
            })
        }else{
            this.apiService.openSnackBar("Please fill all fields.","OK")
        }
    }
}
