"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var designationlist_component_1 = require("./designation-list/designationlist.component");
var designationadd_component_1 = require("./designation-add/designationadd.component");
var designationedit_component_1 = require("./designation-edit/designationedit.component");
exports.DesignationsRoutes = [{
        path: '',
        redirectTo: 'designationlist',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'designationlist',
                component: designationlist_component_1.DesignationListComponent
            }, {
                path: 'add-designation',
                component: designationadd_component_1.DesignationAddComponent
            }, {
                path: 'edit-designation/:id',
                component: designationedit_component_1.DesignationEditComponent
            }]
    }];
//# sourceMappingURL=designations.routing.js.map