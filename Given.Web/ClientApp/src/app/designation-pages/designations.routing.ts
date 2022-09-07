import { Routes } from '@angular/router';

import { DesignationListComponent } from './designation-list/designationlist.component';
import { DesignationAddComponent } from './designation-add/designationadd.component';
import { DesignationEditComponent } from './designation-edit/designationedit.component';

export const DesignationsRoutes: Routes = [{
    path: '',
    redirectTo: 'designationlist',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'designationlist',
        component: DesignationListComponent
    },{
        path: 'add-designation',
        component: DesignationAddComponent
    },{
        path: 'edit-designation/:id',
        component: DesignationEditComponent
    }]
}];
