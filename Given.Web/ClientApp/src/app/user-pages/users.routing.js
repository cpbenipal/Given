"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var user_profile_component_1 = require("./user-profile/user-profile.component");
var userlist_component_1 = require("./user-list/userlist.component");
var userinvite_component_1 = require("./user-invite/userinvite.component");
var changepassword_component_1 = require("./change-password/changepassword.component");
var useredit_component_1 = require("./user-edit/useredit.component");
var companyedit_component_1 = require("./company-edit/companyedit.component");
var userAuth_service_1 = require("../userAuth.service");
exports.UsersRoutes = [{
        path: '',
        redirectTo: 'userlist',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'userprofile/:id',
                component: user_profile_component_1.UserProfileComponent,
                canActivate: [userAuth_service_1.AuthApiService]
            }, {
                path: 'profile/:id',
                component: user_profile_component_1.UserProfileComponent
            }, {
                path: 'userlist',
                component: userlist_component_1.UserListComponent,
                canActivate: [userAuth_service_1.AuthApiService]
            }, {
                path: 'userinvite',
                component: userinvite_component_1.UserInviteComponent,
                canActivate: [userAuth_service_1.AuthApiService]
            }, {
                path: 'changepassword',
                component: changepassword_component_1.ChangePasswordComponent
            }, {
                path: 'company-edit',
                component: companyedit_component_1.CompanyEditComponent,
                canActivate: [userAuth_service_1.AuthApiService]
            }, {
                path: 'useredit/:id',
                component: useredit_component_1.UserEditComponent
            }]
    }];
//# sourceMappingURL=users.routing.js.map