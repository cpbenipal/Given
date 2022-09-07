import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatIconModule, MatOptionModule, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule, MatListModule, MatProgressBarModule, MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { LabelledAnalyticlistComponent } from './labelledAnalytic-list/labelledAnalyticlist.component';

import { labelledAnalyticRoutes } from './labelledAnalytic.routing';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MatCardModule,
        RouterModule.forChild(labelledAnalyticRoutes),
        MatIconModule,
        SharedModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule
    ],
    declarations: [
        LabelledAnalyticlistComponent
    ]
})

export class LabelledAnalyticModule { }
