import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import {NgForm} from '@angular/forms';
import { UUID } from 'angular2-uuid';
@Component({
    selector: 'ms-contactadd',
    templateUrl:'./contactadd-component.html',
    styleUrls: ['./contactadd-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ContactAddComponent implements OnInit {
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
  // roles: Object[] = [];

  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    
  }

  addContact(form: NgForm){
    if(form.valid && this.gender){
      form.value.id = UUID.UUID();
      form.value.userId = this.apiService.getUserDetail('id');
      form.value.gender = this.gender;
      form.value.isActive = this.isActive;
      form.value.isAdd = true;
      form.value.prefix = this.prefix;
      form.value.photo = "";
      
      console.log(JSON.stringify(form.value));
      this.apiService.create('Contacts/PostContact',form.value).subscribe(res=>{
        console.log(res);
        if(res.returnStatus){
          // form.reset();
          form.resetForm();
          this.apiService.openSnackBar(res.returnMessage,"OK");
          this.router.navigate(['/contact/contactlist']);
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



