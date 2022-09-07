import { Component, OnInit,ViewEncapsulation,ViewChild } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../api.service'
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { MatDialog } from '@angular/material';
import { ConfirmationDialogComponent } from '../../shared/components/deteleDialogue';

@Component({
    selector: 'ms-contactlist',
    templateUrl:'./contactlist-component.html',
    styleUrls: ['./contactlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ContactListComponent implements OnInit {
  pageIndex: any = 0;
  pageSize: any = 100;
  totalCount: any = 1;
  orderBy: any = "FirstName";
  orderDir: any = 2; // 1 => Asc / 2  => Desc
 
    public displayedColumns = ['firstName', 'primaryEmail', 'mobile', 'createdOn', 'update', 'delete'];
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
    
  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getUsersList(){
    
    let data = {"query":this.apiService.getUserDetail('id'),"pageIndex":this.pageIndex,"pageSize":this.pageSize,"orderBy":this.orderBy,"orderDir":this.orderDir}

    this.apiService.post('Contacts',data).subscribe(contacts=>{
      this.totalCount = contacts.totalCount
      this.dataSource.data = contacts.items;

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

  redirectToUpdate(contactId){
    this.router.navigateByUrl('/contact/edit-contact/'+contactId);
  }

  redirectToDelete(contactId): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Do you confirm the deletion of this contact?"
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
      if(result) {
        console.log('Yes clicked');
        
        this.apiService.delete('Contacts/'+contactId).subscribe(res=>{
          
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



