import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-labelledReportlist',
    templateUrl:'./labelledReportlist-component.html',
    styleUrls: ['./labelledReportlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LabelledReportlistComponent implements OnInit {
  gifts: Object[] = [];
 
  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    
  }

  
	
}



