// Canvas pipe
import {Pipe, PipeTransform} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
@Pipe({
    name: 'canvasPipe',
    pure: true
})
export class CanvasPipe implements PipeTransform {

    constructor(private domSanitizer: DomSanitizer) {
    }

    transform(_Image: string): string {
        if (_Image) {
            let image: any = 'data:image/png;base64,' + _Image;
            let imageUrl: any = this.domSanitizer.bypassSecurityTrustUrl(image)
            return imageUrl;
        } else {
            return null;
        }
    }
}
