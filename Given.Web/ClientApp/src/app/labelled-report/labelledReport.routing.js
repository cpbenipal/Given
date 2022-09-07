"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var labelledReportlist_component_1 = require("./labelledReport-list/labelledReportlist.component");
exports.LabelledReportRoutes = [{
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'list',
                component: labelledReportlist_component_1.LabelledReportlistComponent
            }]
    }];
//# sourceMappingURL=labelledReport.routing.js.map