"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var contactlist_component_1 = require("./contact-list/contactlist.component");
var contactadd_component_1 = require("./contact-add/contactadd.component");
var contactedit_component_1 = require("./contact-edit/contactedit.component");
exports.ContactsRoutes = [{
        path: '',
        redirectTo: 'contactlist',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'contactlist',
                component: contactlist_component_1.ContactListComponent
            }, {
                path: 'add-contact',
                component: contactadd_component_1.ContactAddComponent
            }, {
                path: 'edit-contact/:id',
                component: contactedit_component_1.ContactEditComponent
            }]
    }];
//# sourceMappingURL=contacts.routing.js.map