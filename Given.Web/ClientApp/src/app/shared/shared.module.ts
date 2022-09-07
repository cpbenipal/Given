import { NgModule } from '@angular/core';

import { MenuItems } from './menu-items/menu-items';
import { HorizontalMenuItems } from './menu-items/horizontal-menu-items';
import { AccordionAnchorDirective, AccordionLinkDirective, AccordionDirective } from './accordion';
import { ToggleFullscreenDirective } from './fullscreen/toggle-fullscreen.directive';
import { CanvasPipe } from './pipes/canvas.pipe';
import { ConfirmationDialogComponent } from './components/deteleDialogue';

@NgModule({
  declarations: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullscreenDirective,
    CanvasPipe,
    ConfirmationDialogComponent
  ],
  exports: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullscreenDirective,
    CanvasPipe
   ],
  entryComponents:[ConfirmationDialogComponent],
  providers: [ MenuItems, HorizontalMenuItems ]
})
export class SharedModule { }
