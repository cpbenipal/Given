"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var login_component_1 = require("./login/login.component");
var register_component_1 = require("./register/register.component");
var userregister_component_1 = require("./userregister/userregister.component");
var forgot_password_component_1 = require("./forgot-password/forgot-password.component");
var lockscreen_component_1 = require("./lockscreen/lockscreen.component");
var verify_component_1 = require("./verify/verify.component");
var confirm_component_1 = require("./confirm/confirm.component");
exports.SessionRoutes = [{
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'login',
                component: login_component_1.LoginComponent
            }, {
                path: 'createcompany',
                component: register_component_1.RegisterComponent
            }, {
                path: 'confirm/:companyid',
                component: confirm_component_1.ConfirmComponent
            },
            {
                path: 'acceptinvitation/:invitedBy/:id',
                component: userregister_component_1.UserRegisterComponent
            }, {
                path: 'forgot-password',
                component: forgot_password_component_1.ForgotPasswordComponent
            }, {
                path: 'lockscreen',
                component: lockscreen_component_1.LockScreenComponent
            }, {
                path: 'confirm-and-update-password',
                component: verify_component_1.VerifyComponent
            }]
    }];
//# sourceMappingURL=session.routing.js.map