"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var labelledAnalyticlist_component_1 = require("./labelledAnalytic-list/labelledAnalyticlist.component");
exports.labelledAnalyticRoutes = [{
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'list',
                component: labelledAnalyticlist_component_1.LabelledAnalyticlistComponent
            }]
    }];
//# sourceMappingURL=labelledAnalytic.routing.js.map