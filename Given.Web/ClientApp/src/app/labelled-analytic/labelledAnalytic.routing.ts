import { Routes } from '@angular/router';

import { LabelledAnalyticlistComponent } from './labelledAnalytic-list/labelledAnalyticlist.component';
export const labelledAnalyticRoutes: Routes = [{
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'list',
        component: LabelledAnalyticlistComponent
    }]
}];
