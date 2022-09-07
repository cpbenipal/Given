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
    selector: 'ms-designationlist',
    templateUrl:'./designationlist-component.html',
    styleUrls: ['./designationlist-component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class DesignationListComponent implements OnInit {
  pageIndex: any = 0;
  pageSize: any = 100;
  totalCount: any = 1;
  orderBy: any = "Name";
  orderDir: any = 2; // 1 => Asc / 2  => Desc
 
  
  allCategories: any = [];
  public displayedColumns = ['name', 'categoryId', 'createdOn', 'status', 'update', 'delete'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort,{static:false}) sort: MatSort;
  @ViewChild(MatPaginator,{static:false}) paginator: MatPaginator;
  
  constructor(
    public dialog: MatDialog,
    private router: Router,
    private apiService: ApiService
    ) {
      this.getDesignationsList();
  }

  ngOnInit() {
    this.apiService.get('Category/GetAllCategory').subscribe(res=>{
      this.allCategories = res;
    })

  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getDesignationsList(){
    
    let data = {"query":this.apiService.getUserDetail('id'),"pageIndex":this.pageIndex,"pageSize":this.pageSize,"orderBy":this.orderBy,"orderDir":this.orderDir}

    this.apiService.post('Designation/GetAll',data).subscribe(designations=>{
      console.log(designations);
      this.totalCount = designations.totalCount
      this.dataSource.data = designations.items;

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

  redirectToUpdate(designationId){
    this.router.navigateByUrl('/designation/edit-designation/'+designationId);
  }

  redirectToDelete(designationId): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Do you confirm the deletion of this designation?"
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
      if(result) {
        console.log('Yes clicked');
        
        this.apiService.delete('Designation/Delete/'+designationId).subscribe(res=>{
          
          this.apiService.openSnackBar(res.returnMessage,'OK');
          if(res.returnStatus){
            this.pageIndex = 0
            this.getDesignationsList();
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

    this.getDesignationsList();
  }
}


@Pipe({
    name: 'returnCategoryPipe',
    pure: true
})
export class ReturnCategoryPipe implements PipeTransform {

    constructor() {
    }

    transform(_Categoryid: string, _CategoryList = []): string {
      console.log(_Categoryid)
      console.log(_CategoryList)
        if (_CategoryList.length < 1) {
            return '';
        }

        if (!_Categoryid) {
            return '';
        }

        const tmpIndex = _CategoryList.findIndex((Category) => {
            return Category.id === _Categoryid;
        });
        if (tmpIndex !== -1) {
            return _CategoryList[tmpIndex].name;
        } else {
            return '';
        }

    }
}





