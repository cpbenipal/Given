import { Routes } from '@angular/router';

import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserListComponent } from './user-list/userlist.component';
import { UserInviteComponent } from './user-invite/userinvite.component';
import { ChangePasswordComponent } from './change-password/changepassword.component';
import { UserEditComponent } from './user-edit/useredit.component';
import { CompanyEditComponent } from './company-edit/companyedit.component';
import { AuthApiService } from '../userAuth.service'
export const UsersRoutes: Routes = [{
    path: '',
    redirectTo: 'userlist',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'userprofile/:id',
        component: UserProfileComponent,
        canActivate:[AuthApiService]
    },{
        path: 'profile/:id',
        component: UserProfileComponent
    }, {
        path: 'userlist',
        component: UserListComponent,
        canActivate:[AuthApiService]
    }, {
        path: 'userinvite',
        component: UserInviteComponent,
        canActivate:[AuthApiService]
    }, {
        path: 'changepassword',
        component: ChangePasswordComponent
    },{
        path: 'company-edit',
        component: CompanyEditComponent,
        canActivate:[AuthApiService]
    }, {
        path: 'useredit/:id',
        component: UserEditComponent
    }]
}];
