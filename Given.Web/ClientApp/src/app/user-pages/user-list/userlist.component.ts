import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-userlist',
    templateUrl:'./userlist-component.html',
    styleUrls: ['./userlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class UserListComponent implements OnInit {
  users: Object[] = [];
  userId: any = this.apiService.getUserDetail('id')
  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      this.getUsersList();
  }

  ngOnInit() {
    
  }

  getUsersList(){
    // alert('j')
    let url: any = '';   
    if(localStorage.getItem('role')=='SuperAdmin'){
        url = 'Users/GetAllUsersBySuperAdmin/' + this.apiService.getUserDetail('companyId');
    }else{
      url = 'Users/GetAllUsersByAdmin/'+this.apiService.getUserDetail('id');
    }
    this.apiService.get(url).subscribe(users=>{
      this.users = users;
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
	
  // users: Object[] = [
  // {
  //     name: 'Haile',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Manager',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face1.jpg'
  //   },{
  //     name: 'Victor',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Developer',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face2.jpg'
  //   },{
  //     name: 'Jhon',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Student',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face3.jpg'
  //   },{
  //     name: 'Maria',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Student',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face4.jpg'
  //   },{
  //      name: 'Marry',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Student',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face5.jpg'
  //   },{
  //     name: 'Love',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Student',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face6.jpg'
  //   },{
  //     name: 'Harris',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Developer',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/face7.jpg'
  //   },{
  //     name: 'Marcus',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Developer',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/test2.jpg'
  //   },{
  //     name: 'Mills',
  //     city: 'New York',
  //     country: 'USA',
  //     post:'Developer',
  //     cover:'assets/images/night.jpg',
  //     image:'assets/images/test3.jpg'
  //   }
  // ];
	
}



