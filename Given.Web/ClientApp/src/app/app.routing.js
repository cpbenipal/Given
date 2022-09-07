"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var admin_layout_component_1 = require("./layouts/admin/admin-layout.component");
var auth_layout_component_1 = require("./layouts/auth/auth-layout.component");
var api_service_1 = require("./api.service");
exports.AppRoutes = [{
        path: '',
        redirectTo: 'authentication',
        pathMatch: 'full',
    }, {
        path: '',
        component: admin_layout_component_1.AdminLayoutComponent,
        children: [{
                path: 'home',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./dashboard/dashboard.module'); }).then(function (m) { return m.DashboardModule; }); },
                canActivate: [api_service_1.ApiService]
            }, {
                path: 'user',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./user-pages/users.module'); }).then(function (m) { return m.UsersModule; }); }
            }, {
                path: 'contact',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./contact-pages/contacts.module'); }).then(function (m) { return m.ContactsModule; }); },
                canActivate: [api_service_1.ApiService]
            }, {
                path: 'designation',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./designation-pages/designations.module'); }).then(function (m) { return m.DesignationsModule; }); },
                canActivate: [api_service_1.ApiService]
            }, {
                path: 'gift',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./gift-pages/gifts.module'); }).then(function (m) { return m.GiftsModule; }); },
                canActivate: [api_service_1.ApiService]
            }, {
                path: 'labelled-report',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./labelled-report/labelledReport.module'); }).then(function (m) { return m.LabelledReportModule; }); },
                canActivate: [api_service_1.ApiService]
            }, {
                path: 'labelled-analytic',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./labelled-analytic/labelledAnalytic.module'); }).then(function (m) { return m.LabelledAnalyticModule; }); },
                canActivate: [api_service_1.ApiService]
            },
            // {
            //   path: 'categories',
            //   loadChildren: () => import('./category-analytic/categories.module').then(m => m.LabelledAnalyticModule),
            //   canActivate: [ApiService]
            // },
            {
                path: 'features',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./features/features.module'); }).then(function (m) { return m.FeaturesModule; }); }
            }, {
                path: 'material',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./material/material.module'); }).then(function (m) { return m.MaterialComponentsModule; }); }
            }, {
                path: 'icons',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./icons/icons.module'); }).then(function (m) { return m.IconsModule; }); }
            },
            {
                path: 'error',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./error/error.module'); }).then(function (m) { return m.ErrorModule; }); }
            },
            {
                path: 'tables',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./tables/tables.module'); }).then(function (m) { return m.TablesModule; }); }
            },
        ]
    }, {
        path: '',
        component: auth_layout_component_1.AuthLayoutComponent,
        children: [{
                path: 'authentication',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./session/session.module'); }).then(function (m) { return m.SessionModule; }); }
            }, {
                path: 'error',
                loadChildren: function () { return Promise.resolve().then(function () { return require('./error/error.module'); }).then(function (m) { return m.ErrorModule; }); }
            }]
    }, {
        path: '**',
        redirectTo: 'session/404'
    }];
//# sourceMappingURL=app.routing.js.map