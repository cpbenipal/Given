import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserRegisterComponent } from './userregister/userregister.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LockScreenComponent } from './lockscreen/lockscreen.component';
import { VerifyComponent } from './verify/verify.component';
import { ConfirmComponent } from './confirm/confirm.component';

export const SessionRoutes: Routes = [{
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
}, {
    path: '',
    children: [{
        path: 'login',
        component: LoginComponent
    }, {
        path: 'createcompany',
        component: RegisterComponent
    }, {
        path: 'confirm/:companyid',
        component: ConfirmComponent
    },
    {
        path: 'acceptinvitation/:invitedBy/:id',
        component: UserRegisterComponent
    }, {
        path: 'forgot-password',
        component: ForgotPasswordComponent
    }, {
        path: 'lockscreen',
        component: LockScreenComponent
    }, {
        path: 'confirm-and-update-password',
        component: VerifyComponent
    }]
}];
