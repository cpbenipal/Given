import { Routes } from '@angular/router';

import { GiftListComponent } from './gift-list/giftlist.component';
import { GiftAddComponent } from './gift-add/giftadd.component';
import { GiftEditComponent } from './gift-edit/giftedit.component';

export const GiftsRoutes: Routes = [{
    path: '',
    redirectTo: 'giftlist',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'giftlist',
        component: GiftListComponent
    },{
        path: 'add-gift',
        component: GiftAddComponent
    },{
        path: 'edit-gift/:id',
        component: GiftEditComponent
    }]
}];
