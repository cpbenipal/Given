import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import {NgForm} from '@angular/forms';
@Component({
    selector: 'ms-changepassword',
    templateUrl:'./changepassword-component.html',
    styleUrls: ['./changepassword-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ChangePasswordComponent implements OnInit {
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;

  roles: Object[] = [];

  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    
  }

  chnagePassword(form: NgForm){
    console.log(form);
    if(form.valid){
      if(form.value.newPassword == form.value.confirmPassword){
        // console.log(this.apiService.getUserDetail('id'));
        form.value.id = this.apiService.getUserDetail('id');
        delete form.value.confirmPassword;

        this.apiService.put('Users/ChangePassword',form.value).subscribe(res=>{
          console.log(res);
          if(res.returnStatus){
            // form.reset();
            form.resetForm();
            this.apiService.openSnackBar(res.returnMessage,"OK");
            this.router.navigate(['/home']);
          }else{
            this.apiService.openSnackBar(res.returnMessage,"OK")
          }
        },err=>{
          console.log(err);             
            // form.resetForm();
                let errorMessage = '';
                if (err.error instanceof ErrorEvent) {
                    // client-side error   
                    errorMessage = `c-s Error Code: ${err.error.message}`;

                } else {
                    // server-side error  
                    errorMessage = `s-s Error Code: ${err.status}\nMessage: ${err.message}`;
                }
                this.apiService.openSnackBar(errorMessage, "OK"); 
            // this.router.navigate(['/home']);          
        })
      }else{
        this.apiService.openSnackBar("New Password and Confrim Password are not matched.","OK")
      }
    }else{
      this.apiService.openSnackBar("Please fill all fields.","OK")
    }
  }
	
  
}



