import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router,ActivatedRoute} from "@angular/router";
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-user-profile',
    templateUrl:'./user-profile-component.html',
    styleUrls: ['./user-profile-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class UserProfileComponent implements OnInit {
  userId: any;
  userDetail:any;
  loggedUserId: any = this.apiService.getUserDetail('id');
  constructor(private apiService: ApiService, private route: ActivatedRoute) {}

  ngOnInit() {
  	this.userId = this.route.snapshot.params.id;
  	this.apiService.getProfile(this.userId).subscribe(res=>{
        this.userDetail = res;
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



