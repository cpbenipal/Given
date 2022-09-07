import { Routes } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth/auth-layout.component';
import { ApiService } from './api.service'
import { AuthApiService } from './userAuth.service'

export const AppRoutes: Routes = [{
    path: '',
    redirectTo: 'authentication',
    pathMatch: 'full',
}, {
    path: '',
    component: AdminLayoutComponent,
    children: [{
        path: 'home',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
        canActivate: [ApiService]
    }, {
        path: 'user',
        loadChildren: () => import('./user-pages/users.module').then(m => m.UsersModule)
    }, {
        path: 'contact',
        loadChildren: () => import('./contact-pages/contacts.module').then(m => m.ContactsModule),
        canActivate: [ApiService]
    }, {
        path: 'designation',
        loadChildren: () => import('./designation-pages/designations.module').then(m => m.DesignationsModule),
        canActivate: [ApiService]
    }, {
        path: 'gift',
        loadChildren: () => import('./gift-pages/gifts.module').then(m => m.GiftsModule),
        canActivate: [ApiService]
    }, {
        path: 'labelled-report',
        loadChildren: () => import('./labelled-report/labelledReport.module').then(m => m.LabelledReportModule),
        canActivate: [ApiService]
    }, {
        path: 'labelled-analytic',
        loadChildren: () => import('./labelled-analytic/labelledAnalytic.module').then(m => m.LabelledAnalyticModule),
        canActivate: [ApiService]
    },
    // {
    //   path: 'categories',
    //   loadChildren: () => import('./category-analytic/categories.module').then(m => m.LabelledAnalyticModule),
    //   canActivate: [ApiService]
    // },
    {
        path: 'features',
        loadChildren: () => import('./features/features.module').then(m => m.FeaturesModule)
    }, {
        path: 'material',
        loadChildren: () => import('./material/material.module').then(m => m.MaterialComponentsModule)
    }, {
        path: 'icons',
        loadChildren: () => import('./icons/icons.module').then(m => m.IconsModule)
    },
    {
        path: 'error',
        loadChildren: () => import('./error/error.module').then(m => m.ErrorModule)
    },
    {
        path: 'tables',
        loadChildren: () => import('./tables/tables.module').then(m => m.TablesModule)
    },
        // {
        //   path: 'charts',
        //   loadChildren: () => import('./chartlib/chartlib.module').then(m => m.ChartlibModule)
        // }, {
        //   path: 'maps',
        //   loadChildren: () => import('./maps/maps.module').then(m => m.MapModule)
        // },{
        //   path: 'cards',
        //   loadChildren: () => import('./cards/cards.module').then(m => m.CardsDemoModule)
        // },{
        //   path: 'pages',
        //   loadChildren: () => import('./custom-pages/pages.module').then(m => m.PagesDemoModule)
        // },{
        //   path: 'user-pages',
        //   loadChildren: () => import('./user-pages/users.module').then(m => m.UsersModule)
        // },{
        //   path: 'gallery',
        //   loadChildren: () => import('./gallery/gallery.module').then(m => m.GalleryDemoModule)
        // },{
        //   path: 'ecommerce',
        //   loadChildren: () => import('./ecommerce/ecommerce.module').then(m => m.EcommerceDemoModule)
        // }
    ]
}, {
    path: '',
    component: AuthLayoutComponent,
    children: [{
        path: 'authentication',
        loadChildren: () => import('./session/session.module').then(m => m.SessionModule)
    }, {
        path: 'error',
        loadChildren: () => import('./error/error.module').then(m => m.ErrorModule)
    }]
}, {
    path: '**',
    redirectTo: 'session/404'
}];
