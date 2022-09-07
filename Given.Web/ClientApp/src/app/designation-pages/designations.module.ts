import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatPaginatorModule,MatSortModule,MatTableModule,MatDatepickerModule, MatIconModule, MatOptionModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatListModule, MatProgressBarModule, MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { DesignationListComponent, ReturnCategoryPipe } from './designation-list/designationlist.component';
import { DesignationAddComponent } from './designation-add/designationadd.component';
import { DesignationEditComponent } from './designation-edit/designationedit.component';

import { DesignationsRoutes } from './designations.routing';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatCardModule,
        RouterModule.forChild(DesignationsRoutes),
        MatIconModule,
        SharedModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule,
        MatDatepickerModule,
        MatTableModule,
        MatSortModule,MatPaginatorModule,MatButtonModule
    ],
    declarations: [
        DesignationListComponent,
        DesignationAddComponent,
        DesignationEditComponent,
        ReturnCategoryPipe
    ],
    exports:[MatTableModule,MatSortModule,MatFormFieldModule,MatButtonModule,
    MatInputModule,MatPaginatorModule]
})

export class DesignationsModule { }
