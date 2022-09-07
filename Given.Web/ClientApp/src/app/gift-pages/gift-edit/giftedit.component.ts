import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from '../../api.service'
import { NgForm } from '@angular/forms';
import * as moment from 'moment';
import {
    
    Pipe,
    PipeTransform
} from '@angular/core';
@Component({
    selector: 'ms-giftedit',
    templateUrl: './giftedit-component.html',
    styleUrls: ['./giftedit-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class GiftEditComponent implements OnInit {
    userId: any;
    giftDetail: any;
  
    keyword = 'firstName';
    keywordD = 'name';
  
    contactId: string = '';
    designationId: string = '';
    
    contactName: string = '';
    designationName: string = '';


    giftDate: string = '';
    amount: string = '';
    description: string = '';
    isAdd: boolean = false;
    today: any = new Date();
  
    allContacts: Object[] = [];
    allDesignations: Object[] = [];
    giftId: any;
    // allRoles: any = [];
    constructor(public apiService: ApiService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        
        

        this.giftId = this.route.snapshot.params.id;
        this.apiService.get('Gift/GetById/'+this.giftId).subscribe(res => {
            console.log(res);
            this.giftDetail = res;
            this.description = res.description;
            this.giftDate = res.giftDate;
            this.amount = res.amount;
            
            this.isAdd = false;
            this.userId = res.userId;

            this.apiService.get('Designation/GetByName').subscribe(res=>{
              this.allDesignations = res;
            })


            this.apiService.get('Contacts/GetByName').subscribe(res=>{
                  this.allContacts = res;

                  setTimeout(()=>{
                        const contactPipe = new ReturnContactPipe();
                        this.contactName = contactPipe.transform(this.giftDetail.contactId,this.allContacts);

                        this.contactId = this.giftDetail.contactId
                        this.designationId = this.giftDetail.designationId

                        const designationPipe = new ReturnDesignationPipe();
                        this.designationName = designationPipe.transform(this.giftDetail.designationId,this.allDesignations);

                    },500)
                    
                })
            
        }, err => {

        })
        
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
     
      onChangeInputC() {
        console.log('contact change');
        this.contactId = '';
      }

      onChangeInputD() {
        console.log('designation change');
        this.designationId = '';
      }
     
      
    updateGift(form: NgForm) {
        if(form.valid && this.contactId!='' && this.designationId!=''){
            form.value.id = this.giftId;
            form.value.userId = this.userId;
            form.value.createdBy = this.userId;
            form.value.updatedBy = this.apiService.getUserDetail('id');
            form.value.contactId = this.contactId;
            form.value.designationId = this.designationId;
            form.value.isAdd = false;
            
            this.apiService.put('Gift/PutGift', form.value).subscribe(res => {
                if (res.returnStatus) {
                    form.resetForm();
                    this.apiService.openSnackBar(res.returnMessage,"OK");
                    this.router.navigate(['/gift/giftlist']);
                } else {
                    this.apiService.openSnackBar(res.returnMessage, 'OK')
                }
            }, err => {
                this.apiService.openSnackBar(err, 'OK')
            })
        }else{
          this.apiService.openSnackBar("Please fill all fields.","OK")
        }
    }
}


@Pipe({
    name: 'returnContactPipe',
    pure: true
})
export class ReturnContactPipe implements PipeTransform {

    constructor() {
    }

    transform(_Contactid: string, _ContactList = []): string {
      console.log(_Contactid)
      console.log(_ContactList)
        if (_ContactList.length < 1) {
            return '';
        }

        if (!_Contactid) {
            return '';
        }

        const tmpIndex = _ContactList.findIndex((Contact) => {
            return Contact.id === _Contactid;
        });
        if (tmpIndex !== -1) {
            return _ContactList[tmpIndex].firstName;
        } else {
            return '';
        }

    }
}

@Pipe({
    name: 'returnDesignationPipe',
    pure: true
})
export class ReturnDesignationPipe implements PipeTransform {

    constructor() {
    }

    transform(_Designationid: string, _DesignationList = []): string {
      console.log(_Designationid)
      console.log(_DesignationList)
        if (_DesignationList.length < 1) {
            return '';
        }

        if (!_Designationid) {
            return '';
        }

        const tmpIndex = _DesignationList.findIndex((Designation) => {
            return Designation.id === _Designationid;
        });
        if (tmpIndex !== -1) {
            return _DesignationList[tmpIndex].name;
        } else {
            return '';
        }

    }
}
