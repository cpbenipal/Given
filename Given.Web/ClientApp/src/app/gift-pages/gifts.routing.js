"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var giftlist_component_1 = require("./gift-list/giftlist.component");
var giftadd_component_1 = require("./gift-add/giftadd.component");
var giftedit_component_1 = require("./gift-edit/giftedit.component");
exports.GiftsRoutes = [{
        path: '',
        redirectTo: 'giftlist',
        pathMatch: 'full',
    }, {
        path: '',
        children: [{
                path: 'giftlist',
                component: giftlist_component_1.GiftListComponent
            }, {
                path: 'add-gift',
                component: giftadd_component_1.GiftAddComponent
            }, {
                path: 'edit-gift/:id',
                component: giftedit_component_1.GiftEditComponent
            }]
    }];
//# sourceMappingURL=gifts.routing.js.map