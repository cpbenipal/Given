import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import {NgForm} from '@angular/forms';
import { UUID } from 'angular2-uuid';
@Component({
    selector: 'ms-designationadd',
    templateUrl:'./designationadd-component.html',
    styleUrls: ['./designationadd-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class DesignationAddComponent implements OnInit {
  categoryId: string = '';
  name: string = '';
  
  status: boolean = false;
  isAdd: boolean = false;
  

  allCategories: any = [];
  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    this.apiService.get('Category/GetAllCategory').subscribe(res=>{
      this.allCategories = res;
    })
    
  }

  addDesignation(form: NgForm){
    if(form.valid && this.categoryId){
      form.value.id = UUID.UUID();
      form.value.userId = this.apiService.getUserDetail('id');
      form.value.createdBy = this.apiService.getUserDetail('id');
      form.value.updatedBy = this.apiService.getUserDetail('id');
      
      form.value.categoryId = this.categoryId;
      form.value.status = this.status;
      form.value.isAdd = true;
      
      
      console.log(JSON.stringify(form.value));
      this.apiService.create('Designation/PostDesignation',form.value).subscribe(res=>{
        console.log(res);
        if(res.returnStatus){
          // form.reset();
          form.resetForm();
          this.apiService.openSnackBar(res.returnMessage,"OK");
          this.router.navigate(['/designation/designationlist']);
        }else{
          this.apiService.openSnackBar(res.returnMessage,"OK")
        }
      },err=>{
        this.apiService.openSnackBar(err,"OK")
      })
    }else{
      this.apiService.openSnackBar("Please fill all fields.","OK")
    }
  }
	
  
}



