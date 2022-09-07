import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
@Component({
    selector: 'ms-labelledAnalyticlist',
    templateUrl:'./labelledAnalyticlist-component.html',
    styleUrls: ['./labelledAnalyticlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LabelledAnalyticlistComponent implements OnInit {
  gifts: Object[] = [];
 
  constructor(
    private router: Router,
    private apiService: ApiService
    ) {
      
  }

  ngOnInit() {
    
  }

  
	
}



