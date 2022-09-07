import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatIconModule, MatOptionModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatListModule, MatProgressBarModule, MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserListComponent } from './user-list/userlist.component';

import { UsersRoutes } from './users.routing';
import { SharedModule } from '../shared/shared.module';
import { UserInviteComponent } from './user-invite/userinvite.component';
import { ChangePasswordComponent } from './change-password/changepassword.component';
import { UserEditComponent } from './user-edit/useredit.component';
import { CompanyEditComponent } from './company-edit/companyedit.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatCardModule,
        RouterModule.forChild(UsersRoutes),
        MatIconModule,
        SharedModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule
    ],
    declarations: [
        UserProfileComponent,
        UserListComponent,
        UserInviteComponent,
        ChangePasswordComponent,
        UserEditComponent,
        CompanyEditComponent
    ]
})

export class UsersModule { }
