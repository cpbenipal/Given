import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import {
    MatCardModule,
    MatInputModule,
    MatRadioModule,
    MatButtonModule,
    MatProgressBarModule,
    MatOptionModule,
    MatSelectModule,
    MatToolbarModule
} from '@angular/material';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserRegisterComponent } from './userregister/userregister.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LockScreenComponent } from './lockscreen/lockscreen.component';
import { VerifyComponent } from './verify/verify.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { SessionRoutes } from './session.routing';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatIconModule,
        RouterModule.forChild(SessionRoutes),
        MatCardModule,
        MatIconModule,
        MatInputModule,
        MatRadioModule,
        MatButtonModule,
        MatProgressBarModule,
        MatToolbarModule,
        FlexLayoutModule,
        MatSelectModule,
        MatOptionModule
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        ForgotPasswordComponent,
        LockScreenComponent,
        VerifyComponent,
        UserRegisterComponent,
        ConfirmComponent
    ]
})

export class SessionModule { }
