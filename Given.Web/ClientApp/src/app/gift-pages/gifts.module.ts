import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatPaginatorModule,MatSortModule,MatTableModule,MatDatepickerModule, MatIconModule, MatOptionModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatListModule, MatProgressBarModule, MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { GiftListComponent } from './gift-list/giftlist.component';
import { GiftAddComponent } from './gift-add/giftadd.component';
import {
    GiftEditComponent, ReturnContactPipe,
    ReturnDesignationPipe } from './gift-edit/giftedit.component';

import { GiftsRoutes } from './gifts.routing';
import { SharedModule } from '../shared/shared.module';
import {AutocompleteLibModule} from 'angular-ng-autocomplete';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatCardModule,
        RouterModule.forChild(GiftsRoutes),
        MatIconModule,
        SharedModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule,
        MatDatepickerModule,
        MatTableModule,
        MatSortModule,MatPaginatorModule,MatButtonModule,AutocompleteLibModule
    ],
    declarations: [
        GiftListComponent,
        GiftAddComponent,
        GiftEditComponent,
        ReturnContactPipe,
        ReturnDesignationPipe
    ],
    exports:[MatTableModule,MatSortModule,MatFormFieldModule,MatButtonModule,
    MatInputModule,MatPaginatorModule]
})

export class GiftsModule { }
