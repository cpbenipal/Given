import { Component, OnInit,ViewEncapsulation,ViewChild } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { MatDialog } from '@angular/material';
import { ConfirmationDialogComponent } from '../../shared/components/deteleDialogue';
import {
    
    Pipe,
    PipeTransform
} from '@angular/core';
@Component({
    selector: 'ms-giftlist',
    templateUrl:'./giftlist-component.html',
    styleUrls: ['./giftlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class GiftListComponent implements OnInit {
  pageIndex: any = 0;
  pageSize: any = 100;
  totalCount: any = 1;
  orderBy: any = "CreatedOn";
  orderDir: any = 2; // 1 => Asc / 2  => Desc
   
  allContacts: Object[] = [];
  allDesignations: Object[] = [];

    public displayedColumns = ['contactId', 'giftDate', 'amount', 'designationId', 'createdOn', 'update', 'delete'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort,{static:false}) sort: MatSort;
  @ViewChild(MatPaginator,{static:false}) paginator: MatPaginator;
  
  constructor(
    public dialog: MatDialog,
    private router: Router,
    private apiService: ApiService
    ) {
      this.getUsersList();
  }

  ngOnInit() {
    this.apiService.get('Designation/GetByName').subscribe(res=>{
      this.allDesignations = res;
    })

    this.apiService.get('Contacts/GetByName').subscribe(res=>{
      this.allContacts = res;
    })
    
  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getUsersList(){
    
    let data = {"query":this.apiService.getUserDetail('id'),"pageIndex":this.pageIndex,"pageSize":this.pageSize,"orderBy":this.orderBy,"orderDir":this.orderDir}

    this.apiService.post('Gift/GetAll',data).subscribe(gifts=>{
      this.totalCount = gifts.totalCount
      this.dataSource.data = gifts.items;

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
	
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  redirectToUpdate(giftId){
    this.router.navigateByUrl('/gift/edit-gift/'+giftId);
  }

  redirectToDelete(giftId): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Do you confirm the deletion of this gift?"
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
      if(result) {
        console.log('Yes clicked');
        
        this.apiService.delete('Gift/Delete/'+giftId).subscribe(res=>{
          
          this.apiService.openSnackBar(res.returnMessage,'OK');
          if(res.returnStatus){
            this.pageIndex = 0
            this.getUsersList();
          }
        })
        // DO SOMETHING
      }
    });
  }


  pageChanged(event){
    console.log(event);
    this.pageIndex = event.pageIndex
    this.pageSize = event.pageSize

    this.getUsersList();
  }
}        