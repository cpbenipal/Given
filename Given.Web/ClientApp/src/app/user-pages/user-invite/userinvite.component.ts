import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import {NgForm} from '@angular/forms';
@Component({
    selector: 'ms-userinvite',
    templateUrl:'./userinvite-component.html',
    styleUrls: ['./userinvite-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class UserInviteComponent implements OnInit {
  formData: any = {
    receiverEmail: null,
    receiverName: null,
    receiverRoleId: null
  }

  roles: Object[] = [];

  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    this.apiService.get('Roles/GetAllRoles').subscribe(roles=>{
      if(localStorage.getItem('role') == 'Administrator'){
        roles.forEach(role=>{
          if(role.roleName=='User'){
            this.roles.push(role);
          }
        })
      }else{
        roles.forEach(role=>{
          if(role.roleName!='SuperAdmin'){
            this.roles.push(role);
          }
        })
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

  sendInvite(form: NgForm){
    if(this.formData.receiverEmail && this.formData.receiverName && this.formData.receiverRoleId){
      console.log(this.apiService.getUserDetail('id'));
      this.formData['invitedBy'] = this.apiService.getUserDetail('id');
      this.formData['currentRoleId'] = this.apiService.getUserDetail('roleId');
      this.formData['sendName'] = this.apiService.getUserDetail('firstName');

      console.log(JSON.stringify(this.formData));
      this.apiService.create('Users/InviteUser',this.formData).subscribe(res=>{
        console.log(res);
        if(res.returnStatus){
          // form.reset();
          form.resetForm();
          this.apiService.openSnackBar(res.returnMessage,"OK");
          this.router.navigate(['/user/userlist']);
        }else{
          this.apiService.openSnackBar(res.returnMessage,"OK")
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
    }else{
      this.apiService.openSnackBar("Please fill all fields.","OK")
    }
  }
	
  
}



