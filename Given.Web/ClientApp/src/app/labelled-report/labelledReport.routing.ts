import { Routes } from '@angular/router';

import { LabelledReportlistComponent } from './labelledReport-list/labelledReportlist.component';
export const LabelledReportRoutes: Routes = [{
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'list',
        component: LabelledReportlistComponent
    }]
}];
