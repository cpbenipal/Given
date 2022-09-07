import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import {NgForm} from '@angular/forms';
import { UUID } from 'angular2-uuid';
@Component({
    selector: 'ms-giftadd',
    templateUrl:'./giftadd-component.html',
    styleUrls: ['./giftadd-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class GiftAddComponent implements OnInit {
  keyword = 'firstName';
  keywordD = 'name';
  
  contactId: string = '';
  description: string = '';
  giftDate: string = '';
  amount: string = '';
  designationId: string = '';
  isAdd: boolean = false;
  today: any = new Date();
  
  allContacts: Object[] = [];
  allDesignations: Object[] = [];

  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  selectEventC(item) {
    console.log('contact selected');
    console.log(item);
    this.contactId = item.id;
    // do something with selected item
  }

  selectEventD(item) {
    console.log('designation selected');
    console.log(item);
    this.designationId = item.id;
    // do something with selected item
  }
 
  onChangeSearchC() {
    console.log('contact clear');
    this.contactId = '';
  }

  onChangeSearchD() {
    console.log('designation clear');
    this.designationId = '';
  }
 
  
  ngOnInit() {
    this.apiService.get('Designation/GetByName').subscribe(res=>{
      this.allDesignations = res;
    })

    this.apiService.get('Contacts/GetByName').subscribe(res=>{
      this.allContacts = res;
    })
    
  }

  addGift(form: NgForm){
    if(form.valid && this.contactId!='' && this.designationId!=''){
      form.value.id = UUID.UUID();
      form.value.userId = this.apiService.getUserDetail('id');
      form.value.createdBy = this.apiService.getUserDetail('id');
      form.value.updatedBy = this.apiService.getUserDetail('id');
      form.value.contactId = this.contactId;
      form.value.designationId = this.designationId;
      
      form.value.isAdd = true;
      
      console.log(JSON.stringify(form.value));
      this.apiService.create('Gift/PostGift',form.value).subscribe(res=>{
        console.log(res);
        if(res.returnStatus){
          // form.reset();
          form.resetForm();
          this.apiService.openSnackBar(res.returnMessage,"OK");
          this.router.navigate(['/gift/giftlist']);
        }else{
          this.apiService.openSnackBar(res.returnMessage,"OK")
        }
      },err=>{
        this.apiService.openSnackBar(err,"OK")
      })
    }else{
      this.apiService.openSnackBar("Please fill all mendatory fields.","OK")
    }
  }
	
  
}



