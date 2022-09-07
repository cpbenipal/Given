import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatPaginatorModule,MatSortModule,MatTableModule,MatDatepickerModule, MatIconModule, MatOptionModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatListModule, MatProgressBarModule, MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { ContactListComponent } from './contact-list/contactlist.component';
import { ContactAddComponent } from './contact-add/contactadd.component';
import { ContactEditComponent } from './contact-edit/contactedit.component';

import { ContactsRoutes } from './contacts.routing';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatCardModule,
        RouterModule.forChild(ContactsRoutes),
        MatIconModule,
        SharedModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule,
        MatTableModule,
        MatSortModule,MatPaginatorModule,MatButtonModule,MatDatepickerModule
    ],
    declarations: [
        ContactListComponent,
        ContactAddComponent,
        ContactEditComponent
    ],
    exports:[MatTableModule,MatSortModule,MatFormFieldModule,MatButtonModule,
    MatInputModule,MatPaginatorModule,MatDatepickerModule]
})

export class ContactsModule { }
