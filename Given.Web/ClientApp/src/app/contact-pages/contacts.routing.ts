import { Routes } from '@angular/router';

import { ContactListComponent } from './contact-list/contactlist.component';
import { ContactAddComponent } from './contact-add/contactadd.component';
import { ContactEditComponent } from './contact-edit/contactedit.component';

export const ContactsRoutes: Routes = [{
    path: '',
    redirectTo: 'contactlist',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'contactlist',
        component: ContactListComponent
    },{
        path: 'add-contact',
        component: ContactAddComponent
    },{
        path: 'edit-contact/:id',
        component: ContactEditComponent
    }]
}];
