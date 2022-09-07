import { Component, OnInit, OnDestroy, ViewChild, HostListener } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { MenuItems } from '../../shared/menu-items/menu-items';
import { HorizontalMenuItems } from '../../shared/menu-items/horizontal-menu-items';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateService } from '@ngx-translate/core';
import PerfectScrollbar from 'perfect-scrollbar';
import {
    PerfectScrollbarConfigInterface,
    PerfectScrollbarComponent, PerfectScrollbarDirective
} from 'ngx-perfect-scrollbar';
import { ApiService } from "../../api.service"
@Component({
    selector: 'app-layout',
    templateUrl: './admin-layout.component.html'
})
export class AdminLayoutComponent implements OnInit, OnDestroy {
    userDetails: any = JSON.parse(localStorage.getItem('userData'));

    private _router: Subscription;

    today: number = Date.now();
    url: string;
    showSettings = false;
    dark: boolean;
    boxed: boolean;
    collapseSidebar: boolean;
    compactSidebar: boolean;
    horizontal: boolean = false;
    sidebarBg: boolean = true;
    currentLang = 'en';
    layoutDir = 'ltr';
    searchFocus: boolean = false;

    menuLayout: any = 'vertical-menu';
    selectedSidebarImage: any = 'bg-1';
    selectedSidebarColor: any = 'sidebar-default';
    selectedHeaderColor: any = 'header-default';
    collapsedClass: any = 'side-panel-opened';

    @ViewChild('sidemenu', { static: true }) sidemenu;
    public config: PerfectScrollbarConfigInterface = {};

    constructor(public apiService: ApiService, private router: Router, public menuItems: MenuItems, public horizontalMenuItems: HorizontalMenuItems, public translate: TranslateService) {
        const browserLang: string = translate.getBrowserLang();
        translate.use(browserLang.match(/en|fr/) ? browserLang : 'en');
    }

    ngOnInit(): void {

        const elemSidebar = <HTMLElement>document.querySelector('.sidebar-container ');

        if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac() && !this.compactSidebar && this.layoutDir != 'rtl') {
            const ps = new PerfectScrollbar(elemSidebar, {
                wheelSpeed: 2,
                suppressScrollX: true
            });
        }

        this._router = this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe((event: NavigationEnd) => {
            this.url = event.url;
            if (this.isOver()) {
                this.sidemenu.close();
            }

            if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac() && !this.compactSidebar && this.layoutDir != 'rtl') {
                // Ps.update(elemContent);
            }
        });
    }

    onFileInput(_Event: any): void {
        const toBase64 = file => {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => resolve(reader.result);
                reader.onerror = error => reject(error);
            });
        };

        this.Main(toBase64);
    }
    // @ts-ignore
    async Main(toBase64: any): any {
        // @ts-ignore
        const file = document.querySelector('#myUpload').files[0];
        const uploadedBase64 = await toBase64(file);
        console.log(uploadedBase64);

        let photo = uploadedBase64.split(',')[1];

        let data = {
            "id": this.apiService.getUserDetail('id'),
            "photo": photo
        }
        this.apiService.postForCheckStatus('Users/UploadPic', data).subscribe(res => {
            console.log(res);
            if (res.returnCode == 200) {
                this.userDetails.photo = photo;
                this.apiService.updateProfileByKey('photo', photo)
                // localStorage.setItem('userData',JSON.stringify(this.userDetails));
                this.apiService.openSnackBar(res.returnMessage, "OK");
                // this.router.navigate(['/home']);
            } else {
                this.apiService.openSnackBar(res.returnMessage, "OK")
            }
        }, err => {
            this.apiService.openSnackBar(err, "OK")
        })
    }

    @HostListener('click', ['$event'])
    onClick(e: any) {
        const elemSidebar = <HTMLElement>document.querySelector('.sidebar-container ');
        setTimeout(() => {
            if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac() && !this.compactSidebar && this.layoutDir != 'rtl') {
                const ps = new PerfectScrollbar(elemSidebar, {
                    wheelSpeed: 2,
                    suppressScrollX: true
                });
            }
        }, 350);
    }

    ngOnDestroy() {
        this._router.unsubscribe();
    }

    isOver(): boolean {
        if (this.url === '/apps/messages' || this.url === '/apps/calendar' || this.url === '/apps/media' || this.url === '/maps/leaflet') {
            return true;
        } else {
            return window.matchMedia(`(max-width: 960px)`).matches;
        }
    }

    isMac(): boolean {
        let bool = false;
        if (navigator.platform.toUpperCase().indexOf('MAC') >= 0 || navigator.platform.toUpperCase().indexOf('IPAD') >= 0) {
            bool = true;
        }
        return bool;
    }

    menuMouseOver(): void {
        if (window.matchMedia(`(min-width: 960px)`).matches && this.collapseSidebar) {
            this.sidemenu.mode = 'over';
        }
    }

    menuMouseOut(): void {
        if (window.matchMedia(`(min-width: 960px)`).matches && this.collapseSidebar) {
            this.sidemenu.mode = 'side';
        }
    }

    menuToggleFunc() {
        this.sidemenu.toggle();

        if (this.collapsedClass == 'side-panel-opened') {
            this.collapsedClass = 'side-panel-closed';
        }
        else {
            this.collapsedClass = 'side-panel-opened';
        }
    }

    changeMenuLayout(value) {
        console.log(value)
        if (value) {
            this.menuLayout = 'top-menu';
        }
        else {
            this.menuLayout = 'vertical-menu';
            this.menuToggleFunc();
        }
    }

    onSelectSidebarImage(selectedClass, event) {
        this.selectedSidebarImage = selectedClass;
    }

    onSelectedSidebarColor(selectedClass) {
        this.selectedSidebarColor = selectedClass;
    }

    onSelectedHeaderColor(selectedClass) {
        this.selectedHeaderColor = selectedClass;
    }

    isBgActive(value) {
        if (value == this.selectedSidebarImage) {
            return true;
        }
        else {
            return false;
        }
    }

    isSidebarActive(value) {
        if (value == this.selectedSidebarColor) {
            return true;
        }
        else {
            return false;
        }
    }

    isHeaderActive(value) {
        if (value == this.selectedHeaderColor) {
            return true;
        }
        else {
            return false;
        }
    }

    checkAccess(menuItem) {
        let role = localStorage.getItem('role')
        if(role){
            if (menuItem.role.indexOf(role.toLowerCase()) > -1) {
                return true;
            } else {
                return false;
            }
        }else{
            return false;
        }
    }

    logout() {
        this.apiService.logout().subscribe(res => {

        }, err => {
            this.apiService.openSnackBar(err, "OK")
        })
        localStorage.clear();
        this.router.navigate(["/authentication/login"])

    }

    // addMenuItem(): void {
    //   this.menuItems.add({
    //     state: 'menu',
    //     name: 'MENU',
    //     type: 'sub',
    //     icon: 'trending_flat',
    //     children: [
    //       {state: 'menu', name: 'MENU'},
    //       {state: 'timelmenuine', name: 'MENU'}
    //     ]
    //   });
    // }
}
